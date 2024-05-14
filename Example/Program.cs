
using ASiNet.Data.Serialization.V2;
using ASiNet.Data.Serialization.V2.Extensions;
using ASiNet.Data.Serialization.V2.IO;

Serializer.Init<int>(x => x.SetIndexer(new TestIndexer())
    .RegisterBaseTypes()
    .RegisterType<A>( ASiNet.Data.Serialization.V2.Enums.DefaultGenerators.Classes)
    .RegisterType<EnTest>( ASiNet.Data.Serialization.V2.Enums.DefaultGenerators.Enums)
    .RegisterType<EnTest[]>( ASiNet.Data.Serialization.V2.Enums.DefaultGenerators.Arrays)
    .RegisterType<int?>( ASiNet.Data.Serialization.V2.Enums.DefaultGenerators.NullableValueTypes)
    .Build());


var buffer = new byte[128];

Serializer.Serialize(new A 
{ 
    Description = "Hello world",
    NullableTest = null, 
    NullableTest1 = 50, 
    Id = 40, 
    ArrTest = [EnTest.A, EnTest.B, EnTest.C],
    Name = "Test Name", EnTest = EnTest.C 
}, 
    (SerializerArrayIO)buffer);

Serializer.Subscribe<A>(x => 
Console.WriteLine($"id: {x?.Id}, name: {x?.Name}, " +
$"d:{x?.Description}, en:{x?.EnTest}, " +
$"arr:[{string.Join(',', x?.ArrTest ?? [])}], n0:{x?.NullableTest}, n0:{x?.NullableTest1}"));

var result = Serializer.DeserializeToEvent((SerializerArrayIO)buffer);

Console.ReadLine();

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


public class A
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? NullableTest { get; set; }

    public int? NullableTest1 { get; set; }

    public EnTest[] ArrTest { get; set; }

    public EnTest EnTest { get; set; }
}

[Flags]
public enum EnTest
{
    A,
    B,
    C
}