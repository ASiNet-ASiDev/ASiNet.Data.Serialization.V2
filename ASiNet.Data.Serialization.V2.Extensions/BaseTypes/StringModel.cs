using System.Text;

namespace ASiNet.Data.Serialization.V2.Extensions.BaseTypes;
public class StringSerializeModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, string>(indexer, key) where TKey : notnull
{
    public override event Action<string?>? OnDeserialize;

    public override string? Deserialize(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var sizeBuff = (stackalloc byte[sizeof(int)]);
        io.ReadBytes(sizeBuff);
        var size = BitConverter.ToInt32(sizeBuff);
        var buff = (stackalloc byte[size]);
        io.ReadBytes(buff);
        var result = Encoding.UTF8.GetString(buff);

        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var sizeBuff = (stackalloc byte[sizeof(int)]);
        io.ReadBytes(sizeBuff);
        var size = BitConverter.ToInt32(sizeBuff);
        var buff = (stackalloc byte[size]);
        io.ReadBytes(buff);
        var result = Encoding.UTF8.GetString(buff);

        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(string? obj, SerializerIO io)
    {
        if (obj is not null)
        {
            io.WriteByte(1);
            var strBytesSize = Encoding.UTF8.GetByteCount(obj);
            var sizeBuff = (stackalloc byte[sizeof(int)]);
            BitConverter.TryWriteBytes(sizeBuff, strBytesSize);
            io.WriteBytes(sizeBuff);
            var buff = (stackalloc byte[strBytesSize]);
            Encoding.UTF8.TryGetBytes(obj, buff, out _);
            io.WriteBytes(buff);
            return;
        }
        io.WriteByte(0);
    }

    public override void SerializeAndWriteIndex(string? obj, SerializerIO io)
    {
        if (obj is not null)
        {
            io.WriteByte(1);
            var strBytesSize = Encoding.UTF8.GetByteCount(obj);
            var sizeBuff = (stackalloc byte[sizeof(int)]);
            BitConverter.TryWriteBytes(sizeBuff, strBytesSize);
            io.WriteBytes(sizeBuff);
            var buff = (stackalloc byte[strBytesSize]);
            Encoding.UTF8.TryGetBytes(obj, buff, out _);
            io.WriteBytes(buff);
            return;
        }
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (obj is string str)
        {
            io.WriteByte(1);
            var strBytesSize = Encoding.UTF8.GetByteCount(str);
            var sizeBuff = (stackalloc byte[sizeof(int)]);
            BitConverter.TryWriteBytes(sizeBuff, strBytesSize);
            io.WriteBytes(sizeBuff);
            var buff = (stackalloc byte[strBytesSize]);
            Encoding.UTF8.TryGetBytes(str, buff, out _);
            io.WriteBytes(buff);
            return;
        }
        io.WriteByte(0);
        return;
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (obj is string str)
        {
            io.WriteByte(1);
            var strBytesSize = Encoding.UTF8.GetByteCount(str);
            var sizeBuff = (stackalloc byte[sizeof(int)]);
            BitConverter.TryWriteBytes(sizeBuff, strBytesSize);
            io.WriteBytes(sizeBuff);
            var buff = (stackalloc byte[strBytesSize]);
            Encoding.UTF8.TryGetBytes(str, buff, out _);
            io.WriteBytes(buff);
            return;
        }
        io.WriteByte(0);
        return;
    }
}