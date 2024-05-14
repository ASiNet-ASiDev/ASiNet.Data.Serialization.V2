using System;
using System.Reflection.PortableExecutable;
using ASiNet.Data.Serialization.V2;
using ASiNet.Data.Serialization.V2.Extensions.BaseTypes.Arrays.Unsafe;

namespace ASiNet.Data.Serialization.Models.Arrays;

file static class ArrayHelper
{
    public static bool IsNullArray<T>(T obj, SerializerIO writer)
    {
        if (obj is null)
        {
            writer.WriteByte(0);
            return true;
        }
        return false;
    }

    public static int ReadLength(SerializerIO reader)
    {
        var lBuff = (stackalloc byte[sizeof(int)]);
        reader.ReadBytes(lBuff);
        return BitConverter.ToInt32(lBuff);
    }

    public static void WriteLength(int length, SerializerIO writer)
    {
        var l = (stackalloc byte[sizeof(int)]);
        BitConverter.TryWriteBytes(l, length);
        writer.WriteBytes(l);
    }

    public static void BlokcCopyElementsUnmanaged<T>(T[] src, int bytesSize, SerializerIO writer) where T : unmanaged
    {
        var buffer = new byte[bytesSize];
        Buffer.BlockCopy(src, 0, buffer, 0, buffer.Length);

        writer.WriteBytes(buffer);
    }

    public static int GetArraySize(int? arrayLength, int? elementSize, bool isNull) =>
        !isNull ? arrayLength!.Value * elementSize!.Value + sizeof(int) + sizeof(byte)
        : sizeof(byte);

}

public class Int32ArrayModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, int[]>(indexer, key) where TKey : notnull
{
    public override event Action<int[]?>? OnDeserialize;

    public override int[]? Deserialize(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(int)];
        io.ReadBytes(bytes);
        var result = bytes.AsInt32Array();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(int)];
        io.ReadBytes(bytes);
        var result = bytes.AsInt32Array();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(int[]? obj, SerializerIO io)
    {
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(int);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeAndWriteIndex(int[]? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(int);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var arr = obj as int[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(int);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var arr = obj as int[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(int);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }
}

public class UInt32ArrayModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, uint[]>(indexer, key) where TKey : notnull
{
    public override event Action<uint[]?>? OnDeserialize;

    public override uint[]? Deserialize(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(uint)];
        io.ReadBytes(bytes);
        var result = bytes.AsUInt32Array();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(uint)];
        io.ReadBytes(bytes);
        var result = bytes.AsUInt32Array();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(uint[]? obj, SerializerIO io)
    {
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(uint);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeAndWriteIndex(uint[]? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(uint);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var arr = obj as uint[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(uint);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var arr = obj as uint[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(uint);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }
}

public class Int16ArrayModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, short[]>(indexer, key) where TKey : notnull
{
    public override event Action<short[]?>? OnDeserialize;

    public override short[]? Deserialize(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(short)];
        io.ReadBytes(bytes);
        var result = bytes.AsInt16Array();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(short)];
        io.ReadBytes(bytes);
        var result = bytes.AsInt16Array();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(short[]? obj, SerializerIO io)
    {
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(short);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeAndWriteIndex(short[]? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(short);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var arr = obj as short[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(short);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var arr = obj as short[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(short);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }
}

public class UInt16ArrayModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, ushort[]>(indexer, key) where TKey : notnull
{
    public override event Action<ushort[]?>? OnDeserialize;

    public override ushort[]? Deserialize(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(ushort)];
        io.ReadBytes(bytes);
        var result = bytes.AsUInt16Array();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(ushort)];
        io.ReadBytes(bytes);
        var result = bytes.AsUInt16Array();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(ushort[]? obj, SerializerIO io)
    {
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(ushort);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeAndWriteIndex(ushort[]? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(ushort);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var arr = obj as ushort[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(ushort);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var arr = obj as ushort[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(ushort);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }
}

public class Int64ArrayModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, long[]>(indexer, key) where TKey : notnull
{
    public override event Action<long[]?>? OnDeserialize;

    public override long[]? Deserialize(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(long)];
        io.ReadBytes(bytes);
        var result = bytes.AsInt64Array();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(long)];
        io.ReadBytes(bytes);
        var result = bytes.AsInt64Array();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(long[]? obj, SerializerIO io)
    {
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(long);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeAndWriteIndex(long[]? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(long);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var arr = obj as long[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(long);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var arr = obj as long[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(long);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }
}

public class UInt64ArrayModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, ulong[]>(indexer, key) where TKey : notnull
{
    public override event Action<ulong[]?>? OnDeserialize;

    public override ulong[]? Deserialize(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(ulong)];
        io.ReadBytes(bytes);
        var result = bytes.AsUInt64Array();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(ulong)];
        io.ReadBytes(bytes);
        var result = bytes.AsUInt64Array();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(ulong[]? obj, SerializerIO io)
    {
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(ulong);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeAndWriteIndex(ulong[]? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(ulong);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var arr = obj as ulong[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(ulong);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var arr = obj as ulong[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(ulong);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }
}

public class CharArrayModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, char[]>(indexer, key) where TKey : notnull
{
    public override event Action<char[]?>? OnDeserialize;

    public override char[]? Deserialize(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(char)];
        io.ReadBytes(bytes);
        var result = bytes.AsCharArray();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(char)];
        io.ReadBytes(bytes);
        var result = bytes.AsCharArray();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(char[]? obj, SerializerIO io)
    {
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(char);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeAndWriteIndex(char[]? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(char);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var arr = obj as char[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(char);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var arr = obj as char[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(char);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }
}

public class BooleanArrayModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, bool[]>(indexer, key) where TKey : notnull
{
    public override event Action<bool[]?>? OnDeserialize;

    public override bool[]? Deserialize(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(bool)];
        io.ReadBytes(bytes);
        var result = bytes.AsBooleanArray();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(bool)];
        io.ReadBytes(bytes);
        var result = bytes.AsBooleanArray();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(bool[]? obj, SerializerIO io)
    {
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(bool);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeAndWriteIndex(bool[]? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(bool);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var arr = obj as bool[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(bool);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var arr = obj as bool[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(bool);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }
}

public class DateTimeArrayModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, DateTime[]>(indexer, key) where TKey : notnull
{
    public override event Action<DateTime[]?>? OnDeserialize;

    public override DateTime[]? Deserialize(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(long)];
        io.ReadBytes(bytes);
        var result = bytes.AsDateTimeArray();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(long)];
        io.ReadBytes(bytes);
        var result = bytes.AsDateTimeArray();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(DateTime[]? obj, SerializerIO io)
    {
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);

        ArrayHelper.WriteLength(obj!.Length, io);
        var dist = new DateTime[obj.Length];
        obj.CopyTo(dist, 0);
        io.WriteBytes(dist.AsByteArray());
    }

    public override void SerializeAndWriteIndex(DateTime[]? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);

        ArrayHelper.WriteLength(obj!.Length, io);
        var dist = new DateTime[obj.Length];
        obj.CopyTo(dist, 0);
        io.WriteBytes(dist.AsByteArray());
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var arr = obj as DateTime[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);

        ArrayHelper.WriteLength(arr!.Length, io);
        var dist = new DateTime[arr!.Length];
        arr!.CopyTo(dist, 0);
        io.WriteBytes(dist.AsByteArray());
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var arr = obj as DateTime[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);

        ArrayHelper.WriteLength(arr!.Length, io);
        var dist = new DateTime[arr!.Length];
        arr!.CopyTo(dist, 0);
        io.WriteBytes(dist.AsByteArray());
    }
}

public class DoubleArrayModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, double[]>(indexer, key) where TKey : notnull
{
    public override event Action<double[]?>? OnDeserialize;

    public override double[]? Deserialize(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(double)];
        io.ReadBytes(bytes);
        var result = bytes.AsDoubleArray();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(double)];
        io.ReadBytes(bytes);
        var result = bytes.AsDoubleArray();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(double[]? obj, SerializerIO io)
    {
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(double);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeAndWriteIndex(double[]? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(double);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var arr = obj as double[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(double);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var arr = obj as double[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(double);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }
}

public class SingleArrayModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, float[]>(indexer, key) where TKey : notnull
{
    public override event Action<float[]?>? OnDeserialize;

    public override float[]? Deserialize(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(float)];
        io.ReadBytes(bytes);
        var result = bytes.AsSingleArray();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(float)];
        io.ReadBytes(bytes);
        var result = bytes.AsSingleArray();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(float[]? obj, SerializerIO io)
    {
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(float);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeAndWriteIndex(float[]? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length * sizeof(float);

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var arr = obj as float[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(float);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var arr = obj as float[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length * sizeof(float);

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }
}

public class GuidArrayModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, Guid[]>(indexer, key) where TKey : notnull
{
    public override event Action<Guid[]?>? OnDeserialize;

    public override Guid[]? Deserialize(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(decimal)];
        io.ReadBytes(bytes);
        var result = bytes.AsGuidArray();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length * sizeof(decimal)];
        io.ReadBytes(bytes);
        var result = bytes.AsGuidArray();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(Guid[]? obj, SerializerIO io)
    {
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);

        ArrayHelper.WriteLength(obj.Length, io);
        var dist = new Guid[obj.Length];
        obj.CopyTo(dist, 0);
        io.WriteBytes(dist.AsByteArray());
    }

    public override void SerializeAndWriteIndex(Guid[]? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);

        ArrayHelper.WriteLength(obj.Length, io);
        var dist = new Guid[obj.Length];
        obj.CopyTo(dist, 0);
        io.WriteBytes(dist.AsByteArray());
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var arr = obj as Guid[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);

        ArrayHelper.WriteLength(arr.Length, io);
        var dist = new Guid[arr!.Length];
        arr!.CopyTo(dist, 0);
        io.WriteBytes(dist.AsByteArray());
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var arr = obj as Guid[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);

        ArrayHelper.WriteLength(arr.Length, io);
        var dist = new Guid[arr!.Length];
        arr!.CopyTo(dist, 0);
        io.WriteBytes(dist.AsByteArray());
    }
}

public class SByteArrayModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, sbyte[]>(indexer, key) where TKey : notnull
{
    public override event Action<sbyte[]?>? OnDeserialize;

    public override sbyte[]? Deserialize(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length];
        io.ReadBytes(bytes);
        var result = bytes.AsSByteArray();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length];
        io.ReadBytes(bytes);
        var result = bytes.AsSByteArray();
        OnDeserialize?.Invoke(result);
        return result;
    }

    public override void Serialize(sbyte[]? obj, SerializerIO io)
    {
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length;

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeAndWriteIndex(sbyte[]? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = obj!.Length;

        ArrayHelper.WriteLength(obj.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(obj, arrBytesLength, io);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var arr = obj as sbyte[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length;

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var arr = obj as sbyte[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);
        var arrBytesLength = arr!.Length;

        ArrayHelper.WriteLength(arr.Length, io);
        ArrayHelper.BlokcCopyElementsUnmanaged(arr, arrBytesLength, io);
    }
}

public class ByteArrayModel<TKey>(ModelsIndexer<TKey> indexer, TKey key) : SerializerModel<TKey, byte[]>(indexer, key) where TKey : notnull
{
    public override event Action<byte[]?>? OnDeserialize;

    public override byte[]? Deserialize(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length];
        io.ReadBytes(bytes);
        return bytes;
    }

    public override object? DeserializeObj(SerializerIO io)
    {
        if (io.ReadByte() == 0)
            return null;
        var length = ArrayHelper.ReadLength(io);
        var bytes = new byte[length];
        io.ReadBytes(bytes);
        return bytes;
    }

    public override void Serialize(byte[]? obj, SerializerIO io)
    {
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);

        ArrayHelper.WriteLength(obj!.Length, io);
        io.WriteBytes(obj);
    }

    public override void SerializeAndWriteIndex(byte[]? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        if (ArrayHelper.IsNullArray(obj, io))
            return;
        io.WriteByte(1);
        
        ArrayHelper.WriteLength(obj!.Length, io);
        io.WriteBytes(obj);
    }

    public override void SerializeObj(object? obj, SerializerIO io)
    {
        var arr = obj as byte[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);

        ArrayHelper.WriteLength(arr!.Length, io);
        io.WriteBytes(arr!);
    }

    public override void SerializeObjAndWriteIndex(object? obj, SerializerIO io)
    {
        Indexer.WriteIndex(Key, io);
        var arr = obj as byte[];
        if (ArrayHelper.IsNullArray(arr, io))
            return;
        io.WriteByte(1);

        ArrayHelper.WriteLength(arr!.Length, io);
        io.WriteBytes(arr!);
    }
}