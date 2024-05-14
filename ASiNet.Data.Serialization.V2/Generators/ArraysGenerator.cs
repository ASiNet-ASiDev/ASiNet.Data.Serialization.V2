using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Expressions.TypesAnalyzer;

namespace ASiNet.Data.Serialization.V2.Generators;
public class ArraysGenerator : LambdasGenerator
{
    public override DeserializeDelegate<TType> GenerateDeserializeLambda<TKey, TType>(Type type, SerializerContext<TKey> context)
    {
        var aa = new ArrayAccess(type);

        var inst = Expression.Parameter(type);
        var io = Expression.Parameter(typeof(SerializerIO));
        var length = Expression.Parameter(typeof(int));

        var model = context.GetModel(aa.ItemType);
        var modelEt = new ExpType(model.GetType());
        var deserializeObjMethodPattern = modelEt.GetMethod(nameof(SerializerModel<byte, byte>.Deserialize))
            .SetParameter(0, io)
            .Build(model);

        var readLengthModel = context.GetModel(typeof(int));
        var readLengthMethod = new ExpType(readLengthModel.GetType()).GetMethod(nameof(SerializerModel<byte, byte>.Deserialize))
            .SetParameter(0, io)
            .Build(readLengthModel);

        var dBody = aa.SetArray(inst).ItemsAccess((index, item) => Expression.Assign(item, deserializeObjMethodPattern))
            .Build();

        var body = Expression.Block([inst, length], [
            Expression.IfThen(
                Helper.ReadNullableByte(io),
                Expression.Block([
                    Expression.Assign(length, readLengthMethod),
                    Expression.Assign(inst, aa.InitArray(length)),
                    dBody])),
            inst
            ]);

        var lambda = Expression.Lambda<DeserializeDelegate<TType>>(body, io);
        return lambda.Compile();
    }

    public override SerializeDelegate<TType> GenerateSerializeLambda<TKey, TType>(Type type, SerializerContext<TKey> context)
    {
        var aa = new ArrayAccess(type);
        var inst = Expression.Parameter(type);
        var io = Expression.Parameter(typeof(SerializerIO));

        var writeLengthModel = context.GetModel(typeof(int));
        var writeLengthMethod = new ExpType(writeLengthModel.GetType()).GetMethod(nameof(SerializerModel<byte, byte>.Serialize))
            .SetParameter(0, Expression.ArrayLength(inst))
            .SetParameter(1, io)
            .Build(writeLengthModel);

        var model = context.GetModel(aa.ItemType);
        var modelEt = new ExpType(model.GetType());
        var serializeObjMethodPattern = modelEt.GetMethod(nameof(SerializerModel<byte, byte>.Serialize))
            .SetParameter(1, io);

        var body = aa.SetArray(inst).ItemsAccess((index, item) => serializeObjMethodPattern.SetParameter(0, item).Build(model)).Build();

        var nullableChecker = Expression.IfThenElse(
            Expression.NotEqual(inst, Expression.Default(type)),
            Expression.Block([Helper.WriteNullableByte(1, io), writeLengthMethod, body]),
            Helper.WriteNullableByte(0, io));

        var lambda = Expression.Lambda<SerializeDelegate<TType>>(nullableChecker, inst, io);
        return lambda.Compile();
    }
}
