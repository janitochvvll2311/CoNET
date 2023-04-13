using System.Buffers;
using System.Text;

namespace CoNET.Services.Rooms;

public class Message
{

    public Memory<byte> Buffer { get; }
    public Memory<byte> BodyBuffer { get; }
    public int Offset { get; }
    public int Length { get; set; }

    public void WriteMessage(string message)
    {
        Length = Encoding.UTF8.GetBytes(message, BodyBuffer.Span);
    }

    public Message(Memory<byte> buffer, int id)
    {
        Buffer = buffer;
        var offset = Encoding.UTF8.GetBytes($"{id}:", Buffer.Span);
        BodyBuffer = Buffer.Slice(offset);
        Offset = offset;
    }

}