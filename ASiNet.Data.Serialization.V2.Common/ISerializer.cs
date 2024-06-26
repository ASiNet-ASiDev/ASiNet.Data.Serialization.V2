﻿namespace ASiNet.Data.Serialization.V2;
public interface ISerializer
{
    public void Serialize<T>(T value, SerializerIO io);

    public T? Deserialize<T>(SerializerIO io);

    public bool DeserializeToEvent(SerializerIO io);

    public bool Subscribe<T>(Action<T> action);

    public bool Unsubscribe<T>(Action<T> action);

    public bool SubscribeTypeNotFound(Action<object> action);

    public bool UnsubscribeTypeNotFound(Action<object> action);
}
