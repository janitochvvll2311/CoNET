using System.Net.WebSockets;

namespace CoNET.Services.Rooms;

public class Member
{
    public WebSocket Socket { get; }
    public bool IsAlive => Socket.State == WebSocketState.Open;

    public async Task SendAsync(Message message)
    {
        await Socket.SendAsync(message.Buffer.Slice(0, message.Offset + message.Length), WebSocketMessageType.Text, true, default);
    }

    public async Task ReceiveAsync(Message message)
    {
        var result = await Socket.ReceiveAsync(message.BodyBuffer, default);
        if (result.MessageType != WebSocketMessageType.Text)
        {
            await Socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, default);
        }
        message.Length = result.Count;
    }

    public Member(WebSocket socket)
    {
        Socket = socket;
    }
}