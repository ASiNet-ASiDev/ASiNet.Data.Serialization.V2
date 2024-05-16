using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace ASiNet.Expressions.TypesAnalyzer;

public class ExpType(Type type)
{
    public Type BaseType { get; } = type;



    public PropertyQuery GetProperty(string name) => 
        new PropertyQuery(BaseType).Select(name);

    public MethodQuery GetMethod(string name) =>
        new MethodQuery(BaseType).Select(name);

    public IEnumerable<PropertyQuery> EnumerateProperties() =>
        BaseType.GetProperties().Select(x => new PropertyQuery(x));

    public IEnumerable<MethodQuery> EnumerateMethods() =>
        BaseType.GetMethods().Select(x => new MethodQuery(x));

    public Expression BuildPropertiesAccess(
        Expression instance, 
        Func<Expression, PropertyQuery, Expression> action)
    {
        var body = Expression.Block(EnumerateProperties().Select(x => action(instance, x)));
        return body;
    }

    public Expression BuildOrderedPropertiesAccess<Tkey>(
        Expression instance, 
        Func<PropertyQuery, Tkey> keySelector, 
        Func<Expression, PropertyQuery, Expression> action)
    {
        var body = Expression.Block(EnumerateProperties().OrderBy(keySelector).Select(x => action(instance, x)));
        return body;
    }

    public Expression BuildOrderedPropertiesAccess<Tkey, Tparameter>(
        Expression instance,
        Tparameter parameter,
        Func<PropertyQuery, Tkey> keySelector,
        Func<Expression, PropertyQuery, Tparameter, Expression> action)
    {
        var body = Expression.Block(EnumerateProperties().OrderBy(keySelector).Select(x => action(instance, x, parameter)));
        return body;
    }

    public Action<Tinstance> BuildPropertiesAccessLambda<Tinstance>(
        Func<Expression, PropertyQuery, Expression> action)
    {
        if(typeof(Tinstance) != BaseType)
            throw new Exception(); // TODO Custom Exception
        var instance = Expression.Parameter(typeof(Tinstance));
        var body = BuildPropertiesAccess(instance, action);
        var lambda = Expression.Lambda<Action<Tinstance>>(body, instance);
        return lambda.Compile();
    }

    public Action<Tinstance> BuildOrderedPropertiesAccessLambda<Tinstance, Tkey>(
        Func<PropertyQuery, Tkey> keySelector,
        Func<Expression, PropertyQuery, Expression> action)
    {
        if (typeof(Tinstance) != BaseType)
            throw new Exception(); // TODO Custom Exception
        var instance = Expression.Parameter(typeof(Tinstance));
        var body = BuildOrderedPropertiesAccess(instance, keySelector, action);
        var lambda = Expression.Lambda<Action<Tinstance>>(body, instance);
        return lambda.Compile();
    }

    public IEnumerable<Type> EnumirateInvolvedTypes()
    {
        if (BaseType.IsArray)
        {
            yield return BaseType.GetElementType()!;
        }
        else if (BaseType.IsEnum)
        {
            yield return BaseType.GetEnumUnderlyingType()!;
        }
        else if (BaseType.IsValueType && Nullable.GetUnderlyingType(BaseType) is Type nut)
        {
            yield return nut;
        }
        else if (BaseType.IsValueType && !BaseType.IsEnum && !BaseType.IsPrimitive)
        {
            foreach (var propertyType in BaseType.GetProperties().Select(x => x.PropertyType))
                yield return propertyType;
        }
        else if (!BaseType.IsArray && !BaseType.IsValueType && !BaseType.IsInterface && !BaseType.IsAbstract && BaseType != typeof(string))
        {
            foreach (var propertyType in BaseType.GetProperties().Select(x => x.PropertyType))
                yield return propertyType;
        }
        yield return BaseType;
    }
}
