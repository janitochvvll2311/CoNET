using System.Net.WebSockets;

namespace CoNET.Services.Rooms;

public class Room
{

    public int Slots { get; }
    public string? Password { get; }
    public string? Tag { get; }
    public Dictionary<int, Member> Members { get; }

    public async Task Join(WebSocket socket, string? password)
    {
        if (Members.Count < Slots && Password == password)
        {
            var member = new Member(socket);
            var id = member.GetHashCode();
            Members[id] = member;
            var buffer = new byte[1024];
            var message = new Message(buffer, id);
            message.WriteMessage("\"Joined\"");
            foreach (var _member in Members.Values)
            {
                await _member.SendAsync(message);
            }
            await Listen(member, message);
            Members.Remove(id);
        }
    }

    public async Task Listen(Member member, Message message)
    {
        while (member.IsAlive)
        {
            await member.ReceiveAsync(message);
            if (message.Length > 0)
            {
                foreach (var _member in Members.Values)
                {
                    await _member.SendAsync(message);
                }
            }
        }
    }

    public Room(int slots, string? password, string? tag)
    {
        Slots = slots;
        Password = password;
        Tag = tag;
        Members = new Dictionary<int, Member>();
    }

}