using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASiNet.Data.Serialization.V2;
public abstract class ModelsIndexer<TKey> where TKey : notnull
{

    public abstract void WriteIndex(TKey key, SerializerIO iO);

    public abstract TKey ReadIndex(SerializerIO iO);

    public abstract TKey OnRegister(Type type);

}
