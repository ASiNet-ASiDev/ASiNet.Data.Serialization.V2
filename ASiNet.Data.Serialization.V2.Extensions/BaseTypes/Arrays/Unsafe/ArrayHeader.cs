using System.Runtime.InteropServices;

namespace ASiNet.Data.Serialization.V2.Extensions.BaseTypes.Arrays.Unsafe;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal struct ArrayHeader
{
    public UIntPtr type;
    public UIntPtr length;
}