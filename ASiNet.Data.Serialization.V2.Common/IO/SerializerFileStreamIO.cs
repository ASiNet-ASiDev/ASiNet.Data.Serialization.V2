namespace ASiNet.Data.Serialization.V2.IO;
public class SerializerFileStreamIO(FileStream stream) : SerializerIO
{
    private FileStream _stream = stream;

    public override byte ReadByte()
    {
        return (byte)_stream.ReadByte();
    }

    public override void ReadBytes(Span<byte> bytes)
    {
        _stream.Read(bytes);
    }

    public override void WriteByte(byte @byte)
    {
        _stream.WriteByte(@byte);
    }

    public override void WriteBytes(Span<byte> bytes)
    {
        _stream.Write(bytes);
    }

    public static implicit operator SerializerFileStreamIO(FileStream src) => new(src);

    public static implicit operator FileStream(SerializerFileStreamIO src) => src._stream;
}
