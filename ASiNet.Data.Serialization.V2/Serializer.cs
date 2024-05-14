namespace ASiNet.Data.Serialization.V2;
public class Serializer<TKey>(SerializerContext<TKey> context) : ISerializer where TKey : notnull
{
    private readonly SerializerContext<TKey> _context = context;


    public void Serialize<T>(T value, SerializerIO io)
    {
        var model = _context.GetModel<T>() ?? throw new NotImplementedException();
        model.SerializeObjAndWriteIndex(value, io);
    }


    public T? Deserialize<T>(SerializerIO io)
    {
        var index = _context.ReadIndex(io);
        var model = _context.GetModel(index) ?? throw new NotImplementedException();
        var result = model.DeserializeObj(io);
        if (result is T value)
            return value;
        throw new NotImplementedException();
    }

    public bool DeserializeToEvent(SerializerIO io)
    {
        try
        {
            var index = _context.ReadIndex(io);
            var model = _context.GetModel(index) ?? throw new NotImplementedException();
            model.DeserializeObj(io);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Subscribe<T>(Action<T> action)
    {
        try
        {
            var model = _context.GetModel<T>();
            if (model is null)
                return false;
            model.Deserialized += action;

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Unsubscribe<T>(Action<T> action)
    {
        try
        {
            var model = _context.GetModel<T>();
            if (model is null)
                return false;
            model.Deserialized -= action;

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool SubscribeTypeNotFound(Action<object> action)
    {
        _context.TypeNotFound += action;
        return true;
    }

    public bool UnsubscribeTypeNotFound(Action<object> action)
    {
        _context.TypeNotFound -= action;
        return true;
    }
}
