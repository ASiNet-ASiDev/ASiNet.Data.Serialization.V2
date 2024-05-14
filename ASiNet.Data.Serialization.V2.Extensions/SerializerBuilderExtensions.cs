using ASiNet.Data.Serialization.Models.Arrays;
using ASiNet.Data.Serialization.V2.Extensions.BaseTypes;

namespace ASiNet.Data.Serialization.V2.Extensions;
public static class SerializerBuilderExtensions
{

    public static ISerializerBuilder<TKey> RegisterBaseTypes<TKey>(this ISerializerBuilder<TKey> builder) where TKey : notnull
    {
        var indexer = builder.Indexer;

        builder.RegisterModel(new ByteSerializeModel<TKey>(indexer, indexer.OnRegister(typeof(byte))))
            .RegisterModel(new SByteSerializeModel<TKey>(indexer, indexer.OnRegister(typeof(sbyte))))

            .RegisterModel(new Int16SerializeModel<TKey>(indexer, indexer.OnRegister(typeof(short))))
            .RegisterModel(new UInt16SerializeModel<TKey>(indexer, indexer.OnRegister(typeof(ushort))))

            .RegisterModel(new Int32SerializeModel<TKey>(indexer, indexer.OnRegister(typeof(int))))
            .RegisterModel(new UInt32SerializeModel<TKey>(indexer, indexer.OnRegister(typeof(uint))))

            .RegisterModel(new Int64SerializeModel<TKey>(indexer, indexer.OnRegister(typeof(long))))
            .RegisterModel(new UInt64SerializeModel<TKey>(indexer, indexer.OnRegister(typeof(ulong))))

            .RegisterModel(new SingleSerializeModel<TKey>(indexer, indexer.OnRegister(typeof(float))))
            .RegisterModel(new DoubleSerializeModel<TKey>(indexer, indexer.OnRegister(typeof(double))))

            .RegisterModel(new StringSerializeModel<TKey>(indexer, indexer.OnRegister(typeof(string))))

            .RegisterModel(new BooleanSerializeModel<TKey>(indexer, indexer.OnRegister(typeof(bool))))
            .RegisterModel(new CharSerializeModel<TKey>(indexer, indexer.OnRegister(typeof(char))))
            .RegisterModel(new DateTimeSerializeModel<TKey>(indexer, indexer.OnRegister(typeof(DateTime))))
            .RegisterModel(new TimeSpanSerializeModel<TKey>(indexer, indexer.OnRegister(typeof(TimeSpan))))
            .RegisterModel(new GuidSerializeModel<TKey>(indexer, indexer.OnRegister(typeof(Guid))));
        return builder;
    }

    public static ISerializerBuilder<TKey> RegisterUnsafeArrays<TKey>(this ISerializerBuilder<TKey> builder) where TKey : notnull
    {
        var indexer = builder.Indexer;

        builder.RegisterModel(new ByteArrayModel<TKey>(indexer, indexer.OnRegister(typeof(byte[]))))
            .RegisterModel(new SByteArrayModel<TKey>(indexer, indexer.OnRegister(typeof(sbyte[]))))

            .RegisterModel(new Int16ArrayModel<TKey>(indexer, indexer.OnRegister(typeof(short[]))))
            .RegisterModel(new UInt16ArrayModel<TKey>(indexer, indexer.OnRegister(typeof(ushort[]))))

            .RegisterModel(new Int32ArrayModel<TKey>(indexer, indexer.OnRegister(typeof(int[]))))
            .RegisterModel(new UInt32ArrayModel<TKey>(indexer, indexer.OnRegister(typeof(uint[]))))

            .RegisterModel(new Int64ArrayModel<TKey>(indexer, indexer.OnRegister(typeof(long[]))))
            .RegisterModel(new UInt64ArrayModel<TKey>(indexer, indexer.OnRegister(typeof(ulong[]))))

            .RegisterModel(new SingleArrayModel<TKey>(indexer, indexer.OnRegister(typeof(float[]))))
            .RegisterModel(new DoubleArrayModel<TKey>(indexer, indexer.OnRegister(typeof(double[]))))
            .RegisterModel(new BooleanArrayModel<TKey>(indexer, indexer.OnRegister(typeof(bool[]))))
            .RegisterModel(new CharArrayModel<TKey>(indexer, indexer.OnRegister(typeof(char[]))))

            .RegisterModel(new DateTimeArrayModel<TKey>(indexer, indexer.OnRegister(typeof(DateTime[]))))
            .RegisterModel(new GuidArrayModel<TKey>(indexer, indexer.OnRegister(typeof(Guid[]))));
        return builder;
    }

}
