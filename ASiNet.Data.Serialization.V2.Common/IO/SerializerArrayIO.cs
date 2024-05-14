namespace ASiNet.Data.Serialization.V2.IO;
public class SerializerArrayIO(byte[] c_src) : SerializerIO
{
    public int Length => _src.Length;

    public int AvalibleBytes => _src.Length - _position;

    public int FilledBytes => _position;

    private int _position;
    private byte[] _src = c_src;

    public override void ReadBytes(Span<byte> data)
    {
        _src.AsSpan().Slice(_position, data.Length).CopyTo(data);
        _position += data.Length;
    }

    public override byte ReadByte()
    {
        return _src[_position++];
    }

    public override void WriteByte(byte data)
    {
        if (AvalibleBytes >= 1)
        {
            _src[_position] = data;
            _position++;
            return;
        }
        throw new IndexOutOfRangeException();
    }

    public override void WriteBytes(Span<byte> data)
    {
        if (AvalibleBytes < data.Length)
            throw new IndexOutOfRangeException();
        data.CopyTo(_src.AsSpan().Slice(_position, data.Length));
        _position += data.Length;
    }

    public byte[] AsArray() => _src;


    public static implicit operator SerializerArrayIO(byte[] src) => new(src);

    public static implicit operator byte[](SerializerArrayIO src) => src._src;
}
