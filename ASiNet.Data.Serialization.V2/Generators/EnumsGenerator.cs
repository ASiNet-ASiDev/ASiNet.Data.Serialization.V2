using System.Linq.Expressions;
using ASiNet.Expressions.TypesAnalyzer;

namespace ASiNet.Data.Serialization.V2.Generators;
public class EnumsGenerator : LambdasGenerator
{
    public override DeserializeDelegate<TType> GenerateDeserializeLambda<TKey, TType>(Type type, SerializerContext<TKey> context)
    {
        var inst = Expression.Parameter(type);
        var io = Expression.Parameter(typeof(SerializerIO));

        var model = context.GetModel(type.GetEnumUnderlyingType());

        var modelEt = new ExpType(model.GetType());
        var deserializeMethod = modelEt.GetMethod(nameof(SerializerModel<byte, byte>.Deserialize)).SetParameter(0, io).Build(model);

        var body = Expression.Block([inst], [
            Expression.Assign(inst, Expression.Convert(deserializeMethod, type)),
            inst
            ]);

        var lambda = Expression.Lambda<DeserializeDelegate<TType>>(body, io);
        return lambda.Compile();
    }

    public override SerializeDelegate<TType> GenerateSerializeLambda<TKey, TType>(Type type, SerializerContext<TKey> context)
    {
        var inst = Expression.Parameter(type);
        var io = Expression.Parameter(typeof(SerializerIO));

        var model = context.GetModel(type.GetEnumUnderlyingType());

        var modelEt = new ExpType(model.GetType());

        var serializeMethod = modelEt.GetMethod(nameof(SerializerModel<byte, byte>.Serialize))
            .SetParameter(0, inst, true)
            .SetParameter(1, io)
            .Build(model);

        var lambda = Expression.Lambda<SerializeDelegate<TType>>(serializeMethod, inst, io);
        return lambda.Compile();
    }
}
