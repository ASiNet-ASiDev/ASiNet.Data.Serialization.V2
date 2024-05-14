namespace ASiNet.Data.Serialization.V2.Extensions.BaseTypes;

public class ByteSerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, byte>(indexer, key) where TKey : notnull
{
    public override event Action<byte>? OnDeserialize;

    public override byte Deserialize(SerializerIO io)
    {
        var result = io.ReadByte();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        var result = io.ReadByte();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(byte obj, SerializerIO io)
    {
        io.WriteByte(obj);
    }

    public override void SerializeAndWriteIndex(byte obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        io.WriteByte(obj);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        io.WriteByte((byte)obj!);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        io.WriteByte((byte)obj!);
    }
}

public class SByteSerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, sbyte>(indexer, key) where TKey : notnull
{
    public override event Action<sbyte>? OnDeserialize;

    public override sbyte Deserialize(SerializerIO io)
    {
        var result = io.ReadByte();
        OnDeserialize?.Invoke((sbyte)result);
        return (sbyte)result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        var result = io.ReadByte();
        OnDeserialize?.Invoke((sbyte)result);
        return (sbyte)result;
    }

    public override void Serialize(sbyte obj, SerializerIO io)
    {
        io.WriteByte((byte)obj);
    }

    public override void SerializeAndWriteIndex(sbyte obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        io.WriteByte((byte)obj);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        io.WriteByte((byte)(sbyte)obj!);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        io.WriteByte((byte)(sbyte)obj!);
    }
}

public class Int16SerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, short>(indexer, key) where TKey : notnull
{
    public override event Action<short>? OnDeserialize;

    public override short Deserialize(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(short)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToInt16(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(short)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToInt16(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(short obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(short)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeAndWriteIndex(short obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(short)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(short)]);
        BitConverter.TryWriteBytes(buffer, (short)obj!);
        io.WriteBytes(buffer);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(short)]);
        BitConverter.TryWriteBytes(buffer, (short)obj!);
        io.WriteBytes(buffer);
    }
}

public class UInt16SerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, ushort>(indexer, key) where TKey : notnull
{
    public override event Action<ushort>? OnDeserialize;

    public override ushort Deserialize(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(ushort)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToUInt16(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(ushort)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToUInt16(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(ushort obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(ushort)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeAndWriteIndex(ushort obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(ushort)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(ushort)]);
        BitConverter.TryWriteBytes(buffer, (ushort)obj!);
        io.WriteBytes(buffer);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(ushort)]);
        BitConverter.TryWriteBytes(buffer, (ushort)obj!);
        io.WriteBytes(buffer);
    }
}

public class Int32SerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, int>(indexer, key) where TKey : notnull
{
    public override event Action<int>? OnDeserialize;

    public override int Deserialize(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(int)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToInt32(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(int)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToInt32(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(int obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(int)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeAndWriteIndex(int obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(int)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(int)]);
        BitConverter.TryWriteBytes(buffer, (int)obj!);
        io.WriteBytes(buffer);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(int)]);
        BitConverter.TryWriteBytes(buffer, (int)obj!);
        io.WriteBytes(buffer);
    }
}

public class UInt32SerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, uint>(indexer, key) where TKey : notnull
{
    public override event Action<uint>? OnDeserialize;

    public override uint Deserialize(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(uint)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToUInt32(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(uint)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToUInt32(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(uint obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(uint)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeAndWriteIndex(uint obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(uint)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(uint)]);
        BitConverter.TryWriteBytes(buffer, (uint)obj!);
        io.WriteBytes(buffer);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(uint)]);
        BitConverter.TryWriteBytes(buffer, (uint)obj!);
        io.WriteBytes(buffer);
    }
}

public class Int64SerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, long>(indexer, key) where TKey : notnull
{
    public override event Action<long>? OnDeserialize;

    public override long Deserialize(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(long)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToInt64(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(long)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToInt64(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(long obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(long)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeAndWriteIndex(long obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(long)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(long)]);
        BitConverter.TryWriteBytes(buffer, (long)obj!);
        io.WriteBytes(buffer);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(long)]);
        BitConverter.TryWriteBytes(buffer, (long)obj!);
        io.WriteBytes(buffer);
    }
}

public class UInt64SerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, ulong>(indexer, key) where TKey : notnull
{
    public override event Action<ulong>? OnDeserialize;

    public override ulong Deserialize(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(ulong)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToUInt64(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(ulong)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToUInt64(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(ulong obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(ulong)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeAndWriteIndex(ulong obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(ulong)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(ulong)]);
        BitConverter.TryWriteBytes(buffer, (ulong)obj!);
        io.WriteBytes(buffer);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(ulong)]);
        BitConverter.TryWriteBytes(buffer, (ulong)obj!);
        io.WriteBytes(buffer);
    }
}

public class SingleSerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, float>(indexer, key) where TKey : notnull
{
    public override event Action<float>? OnDeserialize;

    public override float Deserialize(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(float)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToSingle(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(float)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToSingle(buffer);
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(float obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(float)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeAndWriteIndex(float obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(float)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(float)]);
        BitConverter.TryWriteBytes(buffer, (float)obj!);
        io.WriteBytes(buffer);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(float)]);
        BitConverter.TryWriteBytes(buffer, (float)obj!);
        io.WriteBytes(buffer);
    }
}

public class DoubleSerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, double>(indexer, key) where TKey : notnull
{
    public override event Action<double>? OnDeserialize;

    public override double Deserialize(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(double)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToDouble(buffer);

        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(double)]);
        io.ReadBytes(buffer);
        var result = BitConverter.ToDouble(buffer);

        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(double obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(double)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeAndWriteIndex(double obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(double)]);
        BitConverter.TryWriteBytes(buffer, obj);
        io.WriteBytes(buffer);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var buffer = (stackalloc byte[sizeof(double)]);
        BitConverter.TryWriteBytes(buffer, (double)obj!);
        io.WriteBytes(buffer);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var buffer = (stackalloc byte[sizeof(double)]);
        BitConverter.TryWriteBytes(buffer, (double)obj!);
        io.WriteBytes(buffer);
    }
}