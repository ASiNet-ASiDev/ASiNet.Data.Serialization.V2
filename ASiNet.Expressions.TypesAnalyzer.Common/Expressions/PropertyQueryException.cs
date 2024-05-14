namespace ASiNet.Expressions.TypesAnalyzer.Expressions;
public class PropertyQueryException(string message) : Exception(message)
{


    public static PropertyQueryException PropertyNotFound(string name, Type type) =>
        new($"Property[{name}] not found in type[{type.FullName ?? type.Name}]");

    public static PropertyQueryException PropertyNotSelected() =>
        new($"You did not specify a property");

    public static PropertyQueryException PropertyReadError(string name, Type type) =>
        new($"The selected property[{type.FullName ?? type.Name}.{name}] does not have read permissions");

    public static PropertyQueryException PropertyWriteError(string name, Type type) =>
        new($"The selected property[{type.FullName ?? type.Name}.{name}] does not have write permissions");

    public static PropertyQueryException PropertyLogicalError() =>
        new($"You cannot both set and receive a value at the same time");

    public static PropertyQueryException PropertyValueNotSeted() =>
        new($"You have not specified the value you want to set");

    public static PropertyQueryException PropertyActionNotSeted() =>
        new($"You have not specified the action you want to perform");

    public static PropertyQueryException PropertyInitError() =>
        new($"Failed to create an instance PropertyQuery from the data you provided");

    public static PropertyQueryException PropertyTypesMismath() =>
        new($"You are trying to create a lambda by specifying the wrong types");

}
