using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ASiNet.Expressions.TypesAnalyzer;
public class ArrayAccess
{
    public ArrayAccess(Type arrayType)
    {
        if(!arrayType.IsArray)
            throw new Exception();
        _arrayType = arrayType;
        _itemType = arrayType.GetElementType()!;
    }

    public Type ArrayType => _arrayType;

    public Type ItemType => _itemType;

    private Type _arrayType;

    private Type _itemType;

    private Expression _array = null!;

    private Expression? _body;

    public Expression InitArray(int length)
    {
        return Expression.NewArrayBounds(_itemType, [Expression.Constant(length)]);
    }

    public Expression InitArray(Expression length)
    {
        return Expression.NewArrayBounds(_itemType, [length]);
    }

    public ArrayAccess SetArray(Expression array)
    {
        _array = array;
        return this;
    }

    public ArrayAccess ItemsAccess(Func<Expression, Expression, Expression> action)
    {
        var i = Expression.Parameter(typeof(int), "i");
        var count = Expression.Parameter(typeof(int), "count");
        var breakLabel = Expression.Label("LoopBreak");
        _body = Expression.Block([i, count],
            Expression.Assign(i, Expression.Constant(0)),
            Expression.Assign(count, Expression.ArrayLength(_array)),
            Expression.Loop(
                Expression.IfThenElse(
                    Expression.Equal(i, count),
                    Expression.Break(breakLabel),
                    Expression.Block([
                        action.Invoke(i, Expression.ArrayAccess(_array, i)),
                        Expression.AddAssign(i, Expression.Constant(1))])
                    ),
                breakLabel)
            );
        return this;
    }


    public Expression Build()
    {
        if(_body is null)
            throw new Exception();
        return _body!;
    }
}
