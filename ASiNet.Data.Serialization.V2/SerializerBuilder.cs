namespace ASiNet.Data.Serialization.V2;

public class SerializerBuilder<TKey> : ISerializerBuilder<TKey> where TKey : notnull
{

    private SerializerContext<TKey> _context = new();

    private ModelsIndexer<TKey>? _indexer;

    public ModelsIndexer<TKey> Indexer => _indexer ?? throw new NotImplementedException();

    public ISerializerBuilder<TKey> SetIndexer(ModelsIndexer<TKey> indexer)
    {
        _indexer = indexer;
        _context.SetIndexer(indexer);
        return this;
    }

    public ISerializerBuilder<TKey> RegisterType(Type type, LambdasGenerator generator)
    {
        if (_indexer is null)
            throw new NullReferenceException("Indexer not seted");
        var index = _indexer.OnRegister(type);
        var model = typeof(SerializerModelGeneration<,>).MakeGenericType(typeof(TKey), typeof(TKey));
        var inst = (SerializerModel<TKey>?)Activator.CreateInstance(model, _indexer, index, type, _context, generator) ?? throw new Exception();
        _context.AddModel(inst);
        return this;
    }

    public ISerializerBuilder<TKey> RegisterType<T>(LambdasGenerator generator)
    {
        if (_indexer is null)
            throw new NullReferenceException("Indexer not seted");
        var index = _indexer.OnRegister(typeof(T));
        _context.AddModel(new SerializerModelGeneration<TKey, T>(_indexer, index, typeof(T), _context, generator));
        return this;
    }

    public ISerializerBuilder<TKey> RegisterModel<T>(SerializerModel<TKey, T> model)
    {
        _context.AddModel(model);
        return this;
    }

    public ISerializer Build()
    {
        if(_indexer is null)
            throw new NullReferenceException("Indexer not seted");
        return new Serializer<TKey>(_context);
    }
}
