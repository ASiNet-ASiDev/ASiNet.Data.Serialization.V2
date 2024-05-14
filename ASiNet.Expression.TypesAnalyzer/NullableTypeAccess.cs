using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASiNet.Expressions.TypesAnalyzer;
public class NullableTypeAccess
{
    public NullableTypeAccess(Type type)
    {
        if(type.IsValueType && Nullable.GetUnderlyingType(type) is Type baseType)
        {
            _nullableType = type;
            _baseType = baseType;
        }
        else
            throw new Exception();
    }


    public Type NullableType => _nullableType;

    public Type BaseType => _baseType;

    private Type _nullableType;

    private Type _baseType;

    private Expression? _instance;

    public NullableTypeAccess SetInstance(Expression instance)
    {
        _instance = instance;
        return this;
    }

    public Expression New(Expression value)
    {
        var ctor = _nullableType.GetConstructor(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, [_baseType])!;
        return Expression.New(ctor, value);
    }

    public Expression HashValue() =>
        Expression.Property(_instance!, nameof(Nullable<byte>.HasValue));

    public Expression Value() =>
        Expression.Property(_instance!, nameof(Nullable<byte>.Value));
}
