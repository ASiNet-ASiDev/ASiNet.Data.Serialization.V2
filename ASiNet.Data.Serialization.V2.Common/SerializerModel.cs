using System;

namespace ASiNet.Data.Serialization.V2;

public delegate void SerializeDelegate<TType>(TType? obj, SerializerIO io);

public delegate TType? DeserializeDelegate<TType>(SerializerIO io);

public abstract class SerializerModel<TKey>(ModelsIndexer<TKey> indexer, TKey key, Type type) where TKey : notnull
{
    public Type Type { get; set; } = type;

    public TKey Key { get; } = key;

    public ModelsIndexer<TKey> Indexer { get; } = indexer;

    public abstract void SerializeObj(object? obj, SerializerIO io);

    public abstract object? DeserializeObj(SerializerIO io);

    public abstract void SerializeObjAndWriteIndex(object? obj, SerializerIO io);

    public abstract void SubscribeDeserializeEvent(Delegate action);

    public abstract void UnsubscribeDeserializeEvent(Delegate action);
}

public abstract class SerializerModel<TKey, TType>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey>(indexer, key, typeof(TType)) where TKey : notnull
{
    public abstract event Action<TType?>? Deserialized;

    public abstract TType? Deserialize(SerializerIO io);

    public abstract void Serialize(TType? obj, SerializerIO io);

    public abstract void SerializeAndWriteIndex(TType? obj, SerializerIO io);

    public override void SubscribeDeserializeEvent(Delegate action)
    {
        if(action is Action<TType?> taction)
            Deserialized += taction;
        else
            throw new NotImplementedException();
    }

    public override void UnsubscribeDeserializeEvent(Delegate action)
    {
        if (action is Action<TType?> taction)
            Deserialized -= taction;
        else
            throw new NotImplementedException();
    }

}

public class SerializerModelGeneration<TKey, TType>(ModelsIndexer<TKey> indexer, TKey key, Type type, SerializerContext<TKey> context, LambdasGenerator generator) :
    SerializerModel<TKey, TType>(indexer, key) where TKey : notnull
{

    protected Lazy<SerializeDelegate<TType>> _serializeLambda = new(() => generator.GenerateSerializeLambda<TKey, TType>(type, context));
    protected Lazy<DeserializeDelegate<TType>> _deserializeLambda = new(() => generator.GenerateDeserializeLambda<TKey, TType>(type, context));

    public override event Action<TType?>? Deserialized;

    public override object? DeserializeObj(SerializerIO io)
    {
        var result = _deserializeLambda.Value.Invoke(io);
        Deserialized?.Invoke(result);
        return result;
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        _serializeLambda.Value.Invoke((TType?)obj, io);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        _serializeLambda.Value.Invoke((TType?)obj, io);
    }

    public override TType? Deserialize(SerializerIO io)
    {
        var result = _deserializeLambda.Value.Invoke(io);
        Deserialized?.Invoke(result);
        return result;
    }

    public override void Serialize(TType? obj, SerializerIO io)
    {
        _serializeLambda.Value.Invoke(obj, io);
    }

    public override void SerializeAndWriteIndex(TType? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        _serializeLambda.Value.Invoke(obj, io);
    }
}
