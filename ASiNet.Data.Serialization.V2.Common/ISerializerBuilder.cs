using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialization.V2.Enums;

namespace ASiNet.Data.Serialization.V2;
public interface ISerializerBuilder<TKey> where TKey : notnull
{
    public ModelsIndexer<TKey> Indexer { get; }

    public ISerializerBuilder<TKey> SetIndexer(ModelsIndexer<TKey> indexer);

    public ISerializerBuilder<TKey> RegisterType(Type type, LambdasGenerator generator);

    public ISerializerBuilder<TKey> RegisterType(Type type, DefaultGenerators generator);

    public ISerializerBuilder<TKey> RegisterType<T>(DefaultGenerators generator);

    public ISerializerBuilder<TKey> RegisterType<T>(LambdasGenerator generator);

    public ISerializerBuilder<TKey> RegisterModel<T>(SerializerModel<TKey, T> model);

    public ISerializer Build();

}
