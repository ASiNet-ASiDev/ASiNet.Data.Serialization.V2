using System.Linq.Expressions;
using System.Reflection;
using ASiNet.Expressions.TypesAnalyzer.Expressions;

namespace ASiNet.Expressions.TypesAnalyzer;
public class PropertyQuery
{
    public PropertyQuery(Type type)
    {
        _baseType = type;
    }

    public PropertyQuery(PropertyInfo property)
    {
        _property = property;
        _baseType = property.DeclaringType ?? throw PropertyQueryException.PropertyInitError();
    }

    private Type _baseType;

    private PropertyInfo? _property;

    public Type? _convert;

    private Expression? _value;


    private bool _getValue;

    private bool _setValue;

    public Type BaseType => _baseType;

    public string PropertyName => _property?.Name ?? throw PropertyQueryException.PropertyNotSelected();

    public PropertyInfo Property => _property ?? throw PropertyQueryException.PropertyNotSelected();

    public bool CanRead => _property?.CanRead ?? throw PropertyQueryException.PropertyNotSelected();
    public bool CanWrite => _property?.CanWrite ?? throw PropertyQueryException.PropertyNotSelected();

    public bool ContainsCustomAttribute<T>() where T : Attribute => _property is null ? throw PropertyQueryException.PropertyNotSelected() :
        _property.GetCustomAttribute<T>() is not null;

    public T? GetCustomAttribute<T>() where T : Attribute => _property is null ? throw PropertyQueryException.PropertyNotSelected() :
        _property.GetCustomAttribute<T>();

    public PropertyQuery Select(PropertyInfo property)
    {
        _property = property;
        if (_property is null)
            throw PropertyQueryException.PropertyNotFound(nameof(property), _baseType);
        return this;
    }

    public PropertyQuery Select(string name)
    {
        _property = _baseType.GetProperty(name);
        if (_property is null)
            throw PropertyQueryException.PropertyNotFound(name, _baseType);
        return this;
    }

    public PropertyQuery GetValue()
    {
        if (_setValue)
            throw PropertyQueryException.PropertyLogicalError();
        if (_property is null)
            throw PropertyQueryException.PropertyNotSelected();
        if (!_property.CanRead)
            throw PropertyQueryException.PropertyReadError(_property.Name, _baseType);
        _getValue = true;
        return this;
    }

    public PropertyQuery SetValue(object value)
    {
        if (_getValue)
            throw PropertyQueryException.PropertyLogicalError();
        if (_property is null)
            throw PropertyQueryException.PropertyNotSelected();
        if (!_property.CanWrite)
            throw PropertyQueryException.PropertyWriteError(_property.Name, _baseType);
        _value = Expression.Constant(value);
        _setValue = true;
        return this;
    }

    public PropertyQuery SetValue(object? value, Type type)
    {
        if (_getValue)
            throw PropertyQueryException.PropertyLogicalError();
        if (_property is null)
            throw PropertyQueryException.PropertyNotSelected();
        if (!_property.CanWrite)
            throw PropertyQueryException.PropertyWriteError(_property.Name, _baseType);
        _value = Expression.Constant(value, type);
        _setValue = true;
        return this;
    }

    public PropertyQuery SetValue(Expression value)
    {
        if (_getValue)
            throw PropertyQueryException.PropertyLogicalError();
        if (_property is null)
            throw PropertyQueryException.PropertyNotSelected();
        if (!_property.CanWrite)
            throw PropertyQueryException.PropertyWriteError(_property.Name, _baseType);
        _value = value;
        _setValue = true;
        return this;
    }

    public PropertyQuery Convert(Type type)
    {
        _convert = type;
        return this;
    }

    public PropertyQuery ConvertToPropertyType()
    {
        if (_property is null)
            throw PropertyQueryException.PropertyNotSelected();
        _convert = _property.PropertyType;
        return this;
    }

    public PropertyQuery ConvertToObject()
    {
        _convert = typeof(object);
        return this;
    }

    public Expression Build(object instance) =>
        Build(Expression.Constant(instance));

    public Expression Build(Expression instance)
    {
        if (_property is null)
            throw PropertyQueryException.PropertyNotSelected();
        if (_setValue)
        {
            if (_value is null)
                throw PropertyQueryException.PropertyValueNotSeted();
            if (_convert is not null)
                return Expression.Assign(Expression.Property(instance, _property), Expression.Convert(_value, _convert));
            else
                return Expression.Assign(Expression.Property(instance, _property), _value);
        }
        if (_getValue)
        {
            if(_convert is null)
                return Expression.Property(instance, _property);
            else
                return Expression.Convert(Expression.Property(instance, _property), _convert);
        }
        throw PropertyQueryException.PropertyActionNotSeted();
    }

    public Func<Tinstance, Tresult> BuildGetLambda<Tinstance, Tresult>()
    {
        if (_baseType != typeof(Tinstance))
            throw PropertyQueryException.PropertyTypesMismath();

        if (_convert is null && _property?.PropertyType != typeof(Tresult))
            throw PropertyQueryException.PropertyTypesMismath();

        var instParameter = Expression.Parameter(_baseType);
        GetValue();
        var body = Build(instParameter);
        var lambda = Expression.Lambda<Func<Tinstance, Tresult>>(body, instParameter);
        return lambda.Compile();
    }

    public Func<Tinstance, Tvalue, Tvalue> BuildSetLambda<Tinstance, Tvalue>(bool convertResultToTvalue = false)
    {

        if (_baseType != typeof(Tinstance))
            throw PropertyQueryException.PropertyTypesMismath();

        if (_convert is null && _property?.PropertyType != typeof(Tvalue))
            throw PropertyQueryException.PropertyTypesMismath();

        var instParameter = Expression.Parameter(_baseType);
        var valueParameter = Expression.Parameter(typeof(Tvalue));
        SetValue(valueParameter);
        var body = convertResultToTvalue ? Expression.Convert(Build(instParameter), typeof(Tvalue)) : Build(instParameter);
        var lambda = Expression.Lambda<Func<Tinstance, Tvalue, Tvalue>>(body, instParameter, valueParameter);
        return lambda.Compile();
    }
}
