using CoNET.Utils.Text;
using Microsoft.AspNetCore.Mvc;

namespace CoNET.Services.Rooms;

public static class ControllerExtensions
{

    public static IActionResult GetRoomList<T>(this T controller, string? tag)
        where T : ControllerBase, IRoomController
    {
        var rooms = controller.RoomService.Rooms.AsEnumerable();
        if (tag.IsNotNullOrBlank())
        {
            rooms = rooms.Where(x => x.Value.Tag == tag);
        }
        return controller.Ok(rooms.Select(x => new
        {
            code = x.Key,
            slots = x.Value.Slots,
            members = x.Value.Members.Count,
            password = x.Value.Password.IsNotNullOrBlank(),
            tag = x.Value.Tag,
        }));
    }

    public static IActionResult CreateRoom<T>(this T controller, int slots, string? password, string? tag)
        where T : ControllerBase, IRoomController
    {
        var code = controller.RoomService.CreateRoom(slots, password, tag);
        if (code == null) return controller.NotFound();
        return controller.Ok(code);
    }

    public static async Task JoinRoom<T>(this T controller, int code, string? password)
        where T : ControllerBase, IRoomController
    {
        var socket = await controller.HttpContext.WebSockets.AcceptWebSocketAsync();
        await controller.RoomService.JoinRoom(socket, code, password);
    }

}