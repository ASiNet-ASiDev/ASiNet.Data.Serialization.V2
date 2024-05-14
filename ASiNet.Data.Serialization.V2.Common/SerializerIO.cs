namespace ASiNet.Data.Serialization.V2;
public abstract class SerializerIO
{

    public abstract void WriteBytes(Span<byte> bytes);

    public abstract void ReadBytes(Span<byte> bytes);


    public abstract void WriteByte(byte @byte);

    public abstract byte ReadByte();
}
