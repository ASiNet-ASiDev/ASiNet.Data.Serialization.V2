using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Expressions.TypesAnalyzer;

namespace ASiNet.Data.Serialization.V2.Generators;
internal static class Helper
{

    public static Expression ReadNullableByte(Expression io)
    {
        var et = new ExpType(typeof(SerializerIO));
        var method = et.GetMethod(nameof(SerializerIO.ReadByte))
            .Build(io);
        return Expression.Equal(method, Expression.Constant((byte)1));
    }

    public static Expression WriteNullableByte(byte input, Expression io)
    {
        var et = new ExpType(typeof(SerializerIO));
        var method = et.GetMethod(nameof(SerializerIO.WriteByte))
            .SetParameter(0, input, true)
            .Build(io);
        return method;
    }

}
