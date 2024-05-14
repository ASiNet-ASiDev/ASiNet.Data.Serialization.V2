namespace ASiNet.Data.Serialization.V2.Extensions.BaseTypes;

public class BooleanSerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, bool>(indexer, key) where TKey : notnull
{
    public override event Action<bool>? OnDeserialize;

    public override bool Deserialize(SerializerIO io)
    {
        var result = BitConverter.ToBoolean([io.ReadByte()]);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        var result = BitConverter.ToBoolean([io.ReadByte()]);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(bool obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(bool)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeAndWriteIndex(bool obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(bool)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(bool)]);
        BitConverter.TryWriteBytes(buffer, (bool)obj!);
        io.WriteBytes(buffer);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(bool)]);
        BitConverter.TryWriteBytes(buffer, (bool)obj!);
        io.WriteBytes(buffer);
    }
}

public class CharSerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, char>(indexer, key) where TKey : notnull
{
    public override event Action<char>? OnDeserialize;

    public override char Deserialize(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(char)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToChar(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(char)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToChar(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(char obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(char)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeAndWriteIndex(char obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(char)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(char)]);
        BitConverter.TryWriteBytes(buffer, (char)obj!);
        io.WriteBytes(buffer);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(char)]);
        BitConverter.TryWriteBytes(buffer, (char)obj!);
        io.WriteBytes(buffer);
    }
}

public class DateTimeSerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, DateTime>(indexer, key) where TKey : notnull
{
    public override event Action<DateTime>? OnDeserialize;

    public override DateTime Deserialize(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(long)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToInt64(buffer);
        var dt = DateTime.FromBinary(result);
        OnDeserialize?.Invoke(dt);
        return dt;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(long)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToInt64(buffer);
        var dt = DateTime.FromBinary(result);
        OnDeserialize?.Invoke(dt);
        return dt;
    }

    public override void Serialize(DateTime obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(long)]);
        BitConverter.TryWriteBytes(buffer, obj.ToBinary());
        io.WriteBytes(buffer);
    }

    public override void SerializeAndWriteIndex(DateTime obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(long)]);
        BitConverter.TryWriteBytes(buffer, obj.ToBinary());
        io.WriteBytes(buffer);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(long)]);
        BitConverter.TryWriteBytes(buffer, ((DateTime)obj!).ToBinary());
        io.WriteBytes(buffer);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(long)]);
        BitConverter.TryWriteBytes(buffer, ((DateTime)obj!).ToBinary());
        io.WriteBytes(buffer);
    }
}

public class TimeSpanSerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, TimeSpan>(indexer, key) where TKey : notnull
{
    public override event Action<TimeSpan>? OnDeserialize;

    public override TimeSpan Deserialize(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(long)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToInt64(buffer);
        var dt = TimeSpan.FromTicks(result);
        OnDeserialize?.Invoke(dt);
        return dt;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(long)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToInt64(buffer);
        var dt = TimeSpan.FromTicks(result);
        OnDeserialize?.Invoke(dt);
        return dt;
    }

    public override void Serialize(TimeSpan obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(long)]);
        BitConverter.TryWriteBytes(buffer, obj.Ticks);
        io.WriteBytes(buffer);
    }

    public override void SerializeAndWriteIndex(TimeSpan obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(long)]);
        BitConverter.TryWriteBytes(buffer, obj.Ticks);
        io.WriteBytes(buffer);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(long)]);
        BitConverter.TryWriteBytes(buffer, ((TimeSpan)obj!).Ticks);
        io.WriteBytes(buffer);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(long)]);
        BitConverter.TryWriteBytes(buffer, ((TimeSpan)obj!).Ticks);
        io.WriteBytes(buffer);
    }
}

public class GuidSerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, Guid>(indexer, key) where TKey : notnull
{
    public const int GUID_SIZE = 16;

    public override event Action<Guid>? OnDeserialize;

    public override Guid Deserialize(SerializerIO io)
    {
        var buffer = (stackalloc byte[GUID_SIZE]);
        io.ReadBytes(buffer);
        var result = new Guid(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        var buffer = (stackalloc byte[GUID_SIZE]);
        io.ReadBytes(buffer);
        var result = new Guid(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(Guid obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[GUID_SIZE]);
        obj.TryWriteBytes(buffer);
        io.WriteBytes(buffer);
    }

    public override void SerializeAndWriteIndex(Guid obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[GUID_SIZE]);
        obj.TryWriteBytes(buffer);
        io.WriteBytes(buffer);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[GUID_SIZE]);
        ((Guid)obj!).TryWriteBytes(buffer);
        io.WriteBytes(buffer);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[GUID_SIZE]);
        ((Guid)obj!).TryWriteBytes(buffer);
        io.WriteBytes(buffer);
    }
}