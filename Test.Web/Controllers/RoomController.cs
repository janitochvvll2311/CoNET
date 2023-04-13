using CoNET.Services.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace CoNET.Test.Web.Controllers;

[Route("/api/[controller]")]
public class RoomController : ControllerBase, IRoomController
{

    public RoomService RoomService { get; }

    public RoomController(RoomService roomService)
    {
        RoomService = roomService;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] string? tag)
    {
        return this.GetRoomList(tag);
    }

    [HttpPost]
    public IActionResult Post([FromQuery] int slots, [FromQuery] string? password, [FromQuery] string? tag)
    {
        return this.CreateRoom(slots, password, tag);
    }

    [HttpGet("join")]
    public async Task GetJoin([FromQuery] int code, [FromQuery] string? password)
    {
        await this.JoinRoom(code, password);
    }

}