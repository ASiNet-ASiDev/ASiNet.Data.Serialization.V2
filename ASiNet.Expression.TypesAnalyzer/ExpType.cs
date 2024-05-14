using System.Linq.Expressions;
using System.Reflection;

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
}
