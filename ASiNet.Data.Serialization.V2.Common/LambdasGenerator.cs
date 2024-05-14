namespace ASiNet.Data.Serialization.V2;
public abstract class LambdasGenerator
{

    public abstract SerializeDelegate<TType> GenerateSerializeLambda<TKey, TType>(Type type, SerializerContext<TKey> context) where TKey : notnull;

    public abstract DeserializeDelegate<TType> GenerateDeserializeLambda<TKey, TType>(Type type, SerializerContext<TKey> context) where TKey : notnull;
}
