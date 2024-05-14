using System.Net.Sockets;

namespace ASiNet.Data.Serialization.V2.IO;
public class SerializerNetworkStreamIO(NetworkStream stream) : SerializerIO
{
    public int MaxAttemptsCount { get; set; } = 15;

    public int DelayBetweenAttempts { get; set; } = 100;

    private NetworkStream _stream = stream;

    public override byte ReadByte()
    {
        return (byte)_stream.ReadByte();
    }

    public override void ReadBytes(Span<byte> bytes)
    {
        var attemptsCount = 0;
        while (attemptsCount < MaxAttemptsCount)
        {
            if (_stream.Socket.Available < bytes.Length)
            {
                Task.Delay(DelayBetweenAttempts).Wait();
                attemptsCount++;
                continue;
            }
            else
            {
                _stream.Read(bytes);
                return;
            }
        }
    }

    public override void WriteByte(byte @byte)
    {
        _stream.WriteByte(@byte);
    }

    public override void WriteBytes(Span<byte> bytes)
    {
        _stream.Write(bytes);
    }


    public static implicit operator SerializerNetworkStreamIO(NetworkStream src) => new(src);

    public static implicit operator NetworkStream(SerializerNetworkStreamIO src) => src._stream;
}
