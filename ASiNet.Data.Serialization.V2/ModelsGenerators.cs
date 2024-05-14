namespace ASiNet.Data.Serialization.V2;
internal static class ModelsGenerators
{

    public static LambdasGenerator DefaultGenerator => _defaultGenerator.Value;
    private static Lazy<LambdasGenerator> _defaultGenerator => new(() => new Generators.ObjectsGenerator());

    public static LambdasGenerator EnumsGenerator => _enumsGenerator.Value;
    private static Lazy<LambdasGenerator> _enumsGenerator => new(() => new Generators.EnumsGenerator());

    public static LambdasGenerator ArraysGenerator => _arraysGenerator.Value;
    private static Lazy<LambdasGenerator> _arraysGenerator => new(() => new Generators.ArraysGenerator());

    public static LambdasGenerator NullablesGenerator => _nullablesGenerator.Value;
    private static Lazy<LambdasGenerator> _nullablesGenerator => new(() => new Generators.NullablesGenerator());
}
