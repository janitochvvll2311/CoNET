using System.Net.WebSockets;

namespace CoNET.Services.Rooms;

public class Member
{
    public WebSocket Socket { get; }
    public bool IsAlive => Socket.State == WebSocketState.Open;

    public async Task SendAsync(Message message)
    {
        try
        {
            await Socket.SendAsync(message.Buffer.Slice(0, message.Offset + message.Length), WebSocketMessageType.Text, true, default);
        }
        catch { }
    }

    public async Task ReceiveAsync(Message message)
    {
        message.Length = 0;
        try
        {
            var result = await Socket.ReceiveAsync(message.BodyBuffer, default);
            if (result.MessageType != WebSocketMessageType.Text)
            {
                await Socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, default);
            }
            message.Length = result.Count;
        }
        catch { }
    }

    public Member(WebSocket socket)
    {
        Socket = socket;
    }
}