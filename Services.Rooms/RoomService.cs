using System.Net.WebSockets;

namespace CoNET.Services.Rooms;

public class RoomService
{

    public int Slots { get; }
    public Dictionary<int, Room> Rooms { get; }

    public int? CreateRoom(int slots, string? password, string? tag)
    {
        if (Rooms.Count < Slots)
        {
            var room = new Room(slots, password, tag);
            var code = room.GetHashCode();
            Rooms[code] = room;
            return code;
        }
        return null;
    }

    public async Task JoinRoom(WebSocket socket, int code, string? password)
    {
        var room = Rooms.GetValueOrDefault(code);
        if (room != null)
        {
            await room.Join(socket, password);
        }
    }

    public RoomService(int slots)
    {
        Slots = slots;
        Rooms = new Dictionary<int, Room>();
    }

}