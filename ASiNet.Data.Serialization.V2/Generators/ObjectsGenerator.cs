using System.Linq.Expressions;
using ASiNet.Expressions.TypesAnalyzer;

namespace ASiNet.Data.Serialization.V2.Generators;
public class ObjectsGenerator : LambdasGenerator
{
    public override DeserializeDelegate<TType> GenerateDeserializeLambda<TKey, TType>(Type type, SerializerContext<TKey> context)
    {
        var et = new ExpType(type);

        var inst = Expression.Parameter(type);
        var io = Expression.Parameter(typeof(SerializerIO));

        var dBody = et.BuildOrderedPropertiesAccess(inst, x => x.PropertyName, DeserializeProperties);

        Expression DeserializeProperties(Expression inst, PropertyQuery query)
        {
            var model = context.GetModel(query.Property.PropertyType);

            var modelEt = new ExpType(model.GetType());
            var deserializeObjMethodPattern = modelEt.GetMethod(nameof(SerializerModel<byte, byte>.Deserialize)).SetParameter(0, io);

            return query.SetValue(deserializeObjMethodPattern.Build(model)).ConvertToPropertyType().Build(inst);
        }

        var body = Expression.Block([inst], [
            Expression.IfThen(
                Helper.ReadNullableByte(io),
                Expression.Block([
                    Expression.Assign(inst, Expression.New(type.GetConstructor([])!)),
                    dBody])),
            inst
            ]);

        var lambda = Expression.Lambda<DeserializeDelegate<TType>>(body, io);
        return lambda.Compile();
    }

    public override SerializeDelegate<TType> GenerateSerializeLambda<TKey, TType>(Type type, SerializerContext<TKey> context)
    {
        var et = new ExpType(type);

        var inst = Expression.Parameter(type);
        var io = Expression.Parameter(typeof(SerializerIO));

        var dBody = et.BuildOrderedPropertiesAccess(inst, x => x.PropertyName, SerializeProperties);

        Expression SerializeProperties(Expression inst, PropertyQuery query)
        {
            var model = context.GetModel(query.Property.PropertyType);
            var modelEt = new ExpType(model.GetType());
            var serializeObjMethodPattern = modelEt.GetMethod(nameof(SerializerModel<byte, byte>.Serialize)).SetParameter(1, io);

            return serializeObjMethodPattern.SetParameter(0, query.GetValue().Build(inst)).Build(model);
        }

        var nullableChecker = Expression.IfThenElse(
            Expression.NotEqual(inst, Expression.Default(type)), 
            Expression.Block([Helper.WriteNullableByte(1, io), dBody]), 
            Helper.WriteNullableByte(0, io));

        var lambda = Expression.Lambda<SerializeDelegate<TType>>(nullableChecker, inst, io);
        return lambda.Compile();
    }
}
