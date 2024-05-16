using System.Reflection;
using ASiNet.Data.Serialization.V2.Enums;
using ASiNet.Expressions.TypesAnalyzer;

namespace ASiNet.Data.Serialization.V2;

public class SerializerBuilder<TKey> : ISerializerBuilder<TKey> where TKey : notnull
{

    private SerializerContext<TKey> _context = new();

    private ModelsIndexer<TKey>? _indexer;

    public ModelsIndexer<TKey> Indexer => _indexer ?? throw new NotImplementedException();

    private bool _rtd;

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

        if(!_context.ContainsModel(type))
        {
            var index = _indexer.OnRegister(type);
            var model = typeof(SerializerModelGeneration<,>).MakeGenericType(typeof(TKey), type);
            var inst = (SerializerModel<TKey>?)Activator.CreateInstance(model, _indexer, index, type, _context, generator) ?? throw new Exception();
            _context.AddModel(inst);
        }

        if (_rtd)
        {
            var et = new ExpType(type);
            foreach (var subtype in et.EnumirateInvolvedTypes())
            {
                if (_context.ContainsModel(subtype))
                    continue;
                RegisterType(subtype);
            }
        }
        return this;
    }

    public ISerializerBuilder<TKey> RegisterType<T>(LambdasGenerator generator)
    {
        if (_indexer is null)
            throw new NullReferenceException("Indexer not seted");

        if (!_context.ContainsModel(typeof(T)))
        {
            var index = _indexer.OnRegister(typeof(T));
            _context.AddModel(new SerializerModelGeneration<TKey, T>(_indexer, index, typeof(T), _context, generator));
        }
        if (_rtd)
        {
            var et = new ExpType(typeof(T));
            foreach (var type in et.EnumirateInvolvedTypes())
            {
                if(_context.ContainsModel(type))
                    continue;
                RegisterType(type);
            }
        }
        return this;
    }

    public ISerializerBuilder<TKey> RegisterType(Type type, DefaultGenerators generator = DefaultGenerators.Auto) =>
        RegisterType(type, generator switch
        {
            DefaultGenerators.Auto => ModelsGenerators.Auto(type),
            DefaultGenerators.Structures => ModelsGenerators.StructuresGenerator,
            DefaultGenerators.Classes => ModelsGenerators.ObjectsGenerator,
            DefaultGenerators.Enums => ModelsGenerators.EnumsGenerator,
            DefaultGenerators.Arrays => ModelsGenerators.ArraysGenerator,
            DefaultGenerators.NullableValueTypes => ModelsGenerators.NullablesGenerator,
            _ => throw new NotImplementedException(),
        });

    public ISerializerBuilder<TKey> RegisterType<T>(DefaultGenerators generator = DefaultGenerators.Auto) =>
        RegisterType<T>(generator switch
        {
            DefaultGenerators.Auto => ModelsGenerators.Auto(typeof(T)),
            DefaultGenerators.Structures => ModelsGenerators.StructuresGenerator,
            DefaultGenerators.Classes => ModelsGenerators.ObjectsGenerator,
            DefaultGenerators.Enums => ModelsGenerators.EnumsGenerator,
            DefaultGenerators.Arrays => ModelsGenerators.ArraysGenerator,
            DefaultGenerators.NullableValueTypes => ModelsGenerators.NullablesGenerator,
            _ => throw new NotImplementedException(),
        });

    public ISerializerBuilder<TKey> RegisterModel<T>(SerializerModel<TKey, T> model)
    {
        _context.AddModel(model);
        return this;
    }


    public ISerializerBuilder<TKey> AllowRecursiveTypeDeconstruction()
    {
        _rtd = true;
        return this;
    }

    public ISerializer Build()
    {
        if(_indexer is null)
            throw new NullReferenceException("Indexer not seted");
        return new Serializer<TKey>(_context);
    }
}
