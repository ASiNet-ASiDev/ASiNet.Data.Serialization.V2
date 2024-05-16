using ASiNet.Data.Serialization.V2.Generators;

namespace ASiNet.Data.Serialization.V2;
internal static class ModelsGenerators
{

    public static LambdasGenerator Auto(Type type)
    {
        if(type.IsArray)
            return ArraysGenerator;
        if(type.IsEnum)
            return EnumsGenerator;
        if(type.IsValueType && Nullable.GetUnderlyingType(type) is not null)
            return NullablesGenerator;
        if(!type.IsArray && !type.IsValueType && !type.IsInterface && !type.IsAbstract && type != typeof(string))
            return ObjectsGenerator;
        if(type.IsValueType && !type.IsEnum && !type.IsPrimitive)
            return StructuresGenerator;
        throw new NotImplementedException("Auto generator not found");
    }

    public static LambdasGenerator ObjectsGenerator => _objectsGenerator.Value;
    private static Lazy<LambdasGenerator> _objectsGenerator => new(() => new ObjectsGenerator());

    public static LambdasGenerator StructuresGenerator => _structuresGenerator.Value;
    private static Lazy<LambdasGenerator> _structuresGenerator => new(() => new StructuresGenerator());

    public static LambdasGenerator EnumsGenerator => _enumsGenerator.Value;
    private static Lazy<LambdasGenerator> _enumsGenerator => new(() => new EnumsGenerator());

    public static LambdasGenerator ArraysGenerator => _arraysGenerator.Value;
    private static Lazy<LambdasGenerator> _arraysGenerator => new(() => new ArraysGenerator());

    public static LambdasGenerator NullablesGenerator => _nullablesGenerator.Value;
    private static Lazy<LambdasGenerator> _nullablesGenerator => new(() => new NullablesGenerator());
}
