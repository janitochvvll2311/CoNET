using Microsoft.AspNetCore.Mvc;

namespace CoNET.Services.Rooms.Controllers;

[Route("/api/[controller]")]
public class RoomController : ControllerBase
{

    public RoomService RoomService { get; }

    public RoomController(RoomService roomService)
    {
        RoomService = roomService;
    }

    [HttpPost]
    public IActionResult Post([FromQuery] int slots, [FromQuery] string? password, [FromQuery] string? tag)
    {
        var code = RoomService.CreateRoom(slots, password, tag);
        if (code == null) return NotFound();
        return Ok(code);
    }

    [HttpGet("join")]
    public async Task GetJoin([FromQuery] int code, [FromQuery] string? password)
    {
        var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();
        await RoomService.JoinRoom(socket, code, password);
    }

}