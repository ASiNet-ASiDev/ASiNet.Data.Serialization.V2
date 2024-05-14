using System.Net.Sockets;
using ASiNet.Data.Serialization.V2.IO;

namespace ASiNet.Data.Serialization.V2;
public static class Serializer
{
    private static ISerializer? Current;

    public static bool IsInit => Current is not null;

    public static void Init<TKey>(Action<ISerializerBuilder<TKey>> build) where TKey : notnull
    {
        var builder = new SerializerBuilder<TKey>();
        build(builder);
        Current = builder.Build();
    }

    public static void Serialize<T>(T? obj, byte[] buffer)
    {
        if (Current is null)
            throw new Exception("Serializer not init");
        Current.Serialize(obj!, (SerializerArrayIO)buffer);
    }

    public static T? Deserialize<T>(byte[] buffer)
    {
        if (Current is null)
            throw new Exception("Serializer not init");
        return Current.Deserialize<T>((SerializerArrayIO)buffer);
    }

    public static bool DeserializeToEvent(FileStream buffer)
    {
        if (Current is null)
            throw new Exception("Serializer not init");
        return Current.DeserializeToEvent((SerializerFileStreamIO)buffer);
    }

    public static void Serialize<T>(T? obj, FileStream buffer)
    {
        if (Current is null)
            throw new Exception("Serializer not init");
        Current.Serialize(obj!, (SerializerFileStreamIO)buffer);
    }

    public static T? Deserialize<T>(FileStream buffer)
    {
        if (Current is null)
            throw new Exception("Serializer not init");
        return Current.Deserialize<T>((SerializerFileStreamIO)buffer);
    }

    public static bool DeserializeToEvent(NetworkStream buffer)
    {
        if (Current is null)
            throw new Exception("Serializer not init");
        return Current.DeserializeToEvent((SerializerNetworkStreamIO)buffer);
    }

    public static void Serialize<T>(T? obj, NetworkStream buffer)
    {
        if (Current is null)
            throw new Exception("Serializer not init");
        Current.Serialize(obj!, (SerializerNetworkStreamIO)buffer);
    }

    public static T? Deserialize<T>(NetworkStream buffer)
    {
        if (Current is null)
            throw new Exception("Serializer not init");
        return Current.Deserialize<T>((SerializerNetworkStreamIO)buffer);
    }

    public static bool DeserializeToEvent(byte[] buffer)
    {
        if (Current is null)
            throw new Exception("Serializer not init");
        return Current.DeserializeToEvent((SerializerArrayIO)buffer);
    }

    public static bool Subscribe<T>(Action<T> callback)
    {
        if (Current is null)
            throw new Exception("Serializer not init");
        return Current.Subscribe(callback);
    }
}
