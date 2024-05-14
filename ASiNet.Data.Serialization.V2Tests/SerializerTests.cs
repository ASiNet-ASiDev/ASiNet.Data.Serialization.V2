using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASiNet.Data.Serialization.V2;
using ASiNet.Data.Serialization.V2.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASiNet.Data.Serialization.V2.Tests;

[TestClass()]
public class SerializerTests
{
    [TestMethod()]
    public void SerializeBaseTypesTest()
    {
        Serializer.Init<int>(x => x.SetIndexer(new TestIndexer())
            .RegisterBaseTypes());

        var buffer = new byte[128];
        Serializer.Serialize<int>(10, buffer);
        Assert.AreEqual<int>(10, Serializer.Deserialize<int>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<uint>(20, buffer);
        Assert.AreEqual<uint>(20, Serializer.Deserialize<uint>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<short>(20, buffer);
        Assert.AreEqual<short>(20, Serializer.Deserialize<short>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<ushort>(20, buffer);
        Assert.AreEqual<ushort>(20, Serializer.Deserialize<ushort>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<byte>(20, buffer);
        Assert.AreEqual<byte>(20, Serializer.Deserialize<byte>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<sbyte>(20, buffer);
        Assert.AreEqual<sbyte>(20, Serializer.Deserialize<sbyte>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<double>(20, buffer);
        Assert.AreEqual<double>(20, Serializer.Deserialize<double>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<float>(20, buffer);
        Assert.AreEqual<float>(20, Serializer.Deserialize<float>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<char>('A', buffer);
        Assert.AreEqual<char>('A', Serializer.Deserialize<char>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<bool>(true, buffer);
        Assert.AreEqual<bool>(true, Serializer.Deserialize<bool>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<long>(40, buffer);
        Assert.AreEqual<long>(40, Serializer.Deserialize<long>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<ulong>(40, buffer);
        Assert.AreEqual<ulong>(40, Serializer.Deserialize<ulong>(buffer));


        buffer = new byte[128];
        Serializer.Serialize<string>("abcd", buffer);
        Assert.AreEqual<string>("abcd", Serializer.Deserialize<string>(buffer));

        buffer = new byte[128];
        var dt = DateTime.Now;
        Serializer.Serialize<DateTime>(dt, buffer);
        Assert.AreEqual<DateTime>(dt, Serializer.Deserialize<DateTime>(buffer));

        buffer = new byte[128];
        var ts = TimeSpan.FromHours(20);
        Serializer.Serialize<TimeSpan>(ts, buffer);
        Assert.AreEqual<TimeSpan>(ts, Serializer.Deserialize<TimeSpan>(buffer));

        buffer = new byte[128];
        var gu = Guid.NewGuid();
        Serializer.Serialize<Guid>(gu, buffer);
        Assert.AreEqual<Guid>(gu, Serializer.Deserialize<Guid>(buffer));
    }

    [TestMethod()]
    public void SerializeNullableBaseTypesTest()
    {
        Serializer.Init<int>(x => x.SetIndexer(new TestIndexer())
            .RegisterBaseTypes()
            .RegisterNullableBaseTypes());

        var buffer = new byte[128];
        Serializer.Serialize<int?>(10, buffer);
        Assert.AreEqual<int?>(10, Serializer.Deserialize<int?>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<uint?>(20, buffer);
        Assert.AreEqual<uint?>(20, Serializer.Deserialize<uint?>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<short?>(20, buffer);
        Assert.AreEqual<short?>(20, Serializer.Deserialize<short?>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<ushort?>(20, buffer);
        Assert.AreEqual<ushort?>(20, Serializer.Deserialize<ushort?>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<byte>(20, buffer);
        Assert.AreEqual<byte?>(20, Serializer.Deserialize<byte?>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<sbyte?>(20, buffer);
        Assert.AreEqual<sbyte?>(20, Serializer.Deserialize<sbyte?>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<double?>(20, buffer);
        Assert.AreEqual<double?>(20, Serializer.Deserialize<double?>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<float?>(20, buffer);
        Assert.AreEqual<float?>(20, Serializer.Deserialize<float?>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<char?>('A', buffer);
        Assert.AreEqual<char?>('A', Serializer.Deserialize<char?>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<bool?>(true, buffer);
        Assert.AreEqual<bool?>(true, Serializer.Deserialize<bool?>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<long>(40, buffer);
        Assert.AreEqual<long?>(40, Serializer.Deserialize<long?>(buffer));

        buffer = new byte[128];
        Serializer.Serialize<ulong?>(40, buffer);
        Assert.AreEqual<ulong?>(40, Serializer.Deserialize<ulong?>(buffer));

        buffer = new byte[128];
        var dt = DateTime.Now;
        Serializer.Serialize<DateTime?>(dt, buffer);
        Assert.AreEqual<DateTime?>(dt, Serializer.Deserialize<DateTime?>(buffer));

        buffer = new byte[128];
        var ts = TimeSpan.FromHours(20);
        Serializer.Serialize<TimeSpan?>(ts, buffer);
        Assert.AreEqual<TimeSpan?>(ts, Serializer.Deserialize<TimeSpan?>(buffer));

        buffer = new byte[128];
        var gu = Guid.NewGuid();
        Serializer.Serialize<Guid?>(gu, buffer);
        Assert.AreEqual<Guid?>(gu, Serializer.Deserialize<Guid?>(buffer));
    }


    [TestMethod()]
    public void SerializeUnsafeArraysTest()
    {
        Serializer.Init<int>(x => x.SetIndexer(new TestIndexer())
            .RegisterBaseTypes()
            .RegisterUnsafeArrays());

        var buffer = new byte[128];
        Serializer.Serialize<int[]>([10], buffer);
        Assert.AreEqual<int>(10, Serializer.Deserialize<int[]>(buffer)!.First());

        buffer = new byte[128];
        Serializer.Serialize<uint[]>([20], buffer);
        Assert.AreEqual<uint>(20, Serializer.Deserialize<uint[]>(buffer)!.First());

        buffer = new byte[128];
        Serializer.Serialize<short[]>([20], buffer);
        Assert.AreEqual<short>(20, Serializer.Deserialize<short[]>(buffer)!.First());

        buffer = new byte[128];
        Serializer.Serialize<ushort[]>([20], buffer);
        Assert.AreEqual<ushort>(20, Serializer.Deserialize<ushort[]>(buffer)!.First());

        buffer = new byte[128];
        Serializer.Serialize<byte[]>([20], buffer);
        Assert.AreEqual<byte>(20, Serializer.Deserialize<byte[]>(buffer)!.First());

        buffer = new byte[128];
        Serializer.Serialize<sbyte[]>([20], buffer);
        Assert.AreEqual<sbyte>(20, Serializer.Deserialize<sbyte[]>(buffer)!.First());

        buffer = new byte[128];
        Serializer.Serialize<double[]>([20], buffer);
        Assert.AreEqual<double>(20, Serializer.Deserialize<double[]>(buffer)!.First());

        buffer = new byte[128];
        Serializer.Serialize<float[]>([20], buffer);
        Assert.AreEqual<float>(20, Serializer.Deserialize<float[]>(buffer)!.First());

        buffer = new byte[128];
        Serializer.Serialize<char[]>(['A'], buffer);
        Assert.AreEqual<char>('A', Serializer.Deserialize<char[]>(buffer)!.First());

        buffer = new byte[128];
        Serializer.Serialize<bool[]>([true], buffer);
        Assert.AreEqual<bool>(true, Serializer.Deserialize<bool[]>(buffer)!.First());

        buffer = new byte[128];
        Serializer.Serialize<long[]>([40], buffer);
        Assert.AreEqual<long>(40, Serializer.Deserialize<long[]>(buffer)!.First());

        buffer = new byte[128];
        Serializer.Serialize<ulong[]>([40], buffer);
        Assert.AreEqual<ulong>(40, Serializer.Deserialize<ulong[]>(buffer)!.First());

        buffer = new byte[128];
        var dt = DateTime.Now;
        Serializer.Serialize<DateTime[]>([dt], buffer);
        Assert.AreEqual<DateTime>(dt, Serializer.Deserialize<DateTime[]>(buffer)!.First());

        buffer = new byte[128];
        var gu = Guid.NewGuid();
        Serializer.Serialize<Guid[]>([gu], buffer);
        Assert.AreEqual<Guid>(gu, Serializer.Deserialize<Guid[]>(buffer)!.First());
    }
}

public class TestIndexer : ModelsIndexer<int>
{
    public int _lastIndex;

    public override int OnRegister(Type type)
    {
        _lastIndex += 1;
        return _lastIndex;
    }

    public override int ReadIndex(SerializerIO iO)
    {
        var buff = (stackalloc byte[sizeof(int)]);
        iO.ReadBytes(buff);
        return BitConverter.ToInt32(buff);
    }

    public override void WriteIndex(int key, SerializerIO iO)
    {
        var buff = (stackalloc byte[sizeof(int)]);
        BitConverter.TryWriteBytes(buff, key);
        iO.WriteBytes(buff);
    }
}