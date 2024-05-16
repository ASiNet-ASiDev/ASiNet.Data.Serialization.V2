namespace ASiNet.Data.Serialization.V2;

public class SerializerContext<TKey>() where TKey : notnull
{

    private Dictionary<TKey, SerializerModel<TKey>> _models = [];
    private Dictionary<Type, SerializerModel<TKey>> _typeModelsPair = [];

    public ModelsIndexer<TKey> Indexer { get; private set; } = null!;

    public Action<object>? TypeNotFound;

    public void SetIndexer(ModelsIndexer<TKey> indexer)
    {
        if(Indexer == null)
            Indexer = indexer;
        else
            throw new NotImplementedException();
    }

    public bool ContainsModel<TType>()
    {
        return _typeModelsPair.ContainsKey(typeof(TType));
    }

    public bool ContainsModel(Type type)
    {
        return _typeModelsPair.ContainsKey(type);
    }

    public bool ContainsModel(TKey key)
    {
        return _models.ContainsKey(key);
    }

    public void AddModel<TType>(SerializerModel<TKey, TType> model)
    {
        _models.Add(model.Key, model);
        _typeModelsPair.Add(model.Type, model);
    }

    public void AddModel(SerializerModel<TKey> model)
    {
        _models.Add(model.Key, model);
        _typeModelsPair.Add(model.Type, model);
    }

    public SerializerModel<TKey, TType> GetModel<TType>(TKey key)
    {
        if (_models.TryGetValue(key, out var model))
            return model as SerializerModel<TKey, TType> ?? throw new Exception();
        TypeNotFound?.Invoke(key);

        throw new NotImplementedException();
    }



    public SerializerModel<TKey, TType> GetModel<TType>()
    {
        if (_typeModelsPair.TryGetValue(typeof(TType), out var model))
            return model as SerializerModel<TKey, TType> ?? throw new Exception();
        TypeNotFound?.Invoke(typeof(TType));
        throw new NotImplementedException();
    }

    public SerializerModel<TKey> GetModel(Type type)
    {
        if (_typeModelsPair.TryGetValue(type, out var model))
            return model;
        TypeNotFound?.Invoke(type);
        throw new NotImplementedException();
    }

    public SerializerModel<TKey> GetModel(TKey key)
    {
        if (_models.TryGetValue(key, out var model))
            return model;

        TypeNotFound?.Invoke(key);
        throw new NotImplementedException();
    }

    public TKey ReadIndex(SerializerIO io)
    {
        return Indexer.ReadIndex(io);
    }

    public void WriteIndex(TKey index, SerializerIO io)
    {
        Indexer.WriteIndex(index, io);
    }

    public TKey ReadIndexObj(SerializerIO io)
    {
        return Indexer.ReadIndex(io);
    }

    public void WriteIndexObj(TKey index, SerializerIO io)
    {
        Indexer.WriteIndex(index, io);
    }
}
