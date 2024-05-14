using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Expressions.TypesAnalyzer;

namespace ASiNet.Data.Serialization.V2.Generators;
public class NullablesGenerator : LambdasGenerator
{
    public override DeserializeDelegate<TType> GenerateDeserializeLambda<TKey, TType>(Type type, SerializerContext<TKey> context)
    {
        var inst = Expression.Parameter(type);
        var io = Expression.Parameter(typeof(SerializerIO));

        var na = new NullableTypeAccess(type)
            .SetInstance(inst);

        var model = context.GetModel(na.BaseType);
        var modelEt = new ExpType(model.GetType());
        var deserializeObjMethod = modelEt.GetMethod(nameof(SerializerModel<byte, byte>.Deserialize))
            .SetParameter(0, io)
            .Build(model);

        var body = Expression.Block([inst], [
            Expression.IfThen(
                Helper.ReadNullableByte(io),
                Expression.Block([
                    Expression.Assign(inst, na.New(deserializeObjMethod))])),
            inst
            ]);

        var lambda = Expression.Lambda<DeserializeDelegate<TType>>(body, io);
        return lambda.Compile();
    }

    public override SerializeDelegate<TType> GenerateSerializeLambda<TKey, TType>(Type type, SerializerContext<TKey> context)
    {
        var inst = Expression.Parameter(type);
        var io = Expression.Parameter(typeof(SerializerIO));

        var na = new NullableTypeAccess(type)
            .SetInstance(inst);

        var model = context.GetModel(na.BaseType);
        var modelEt = new ExpType(model.GetType());
        var deserializeObjMethod = modelEt.GetMethod(nameof(SerializerModel<byte, byte>.Serialize))
            .SetParameter(1, io);

        var body = deserializeObjMethod.SetParameter(0, na.Value()).Build(model);

        var nullableChecker = Expression.IfThenElse(
            Expression.NotEqual(inst, Expression.Default(type)),
            Expression.Block([Helper.WriteNullableByte(1, io), body]),
            Helper.WriteNullableByte(0, io));

        var lambda = Expression.Lambda<SerializeDelegate<TType>>(nullableChecker, inst, io);
        return lambda.Compile();
    }
}
