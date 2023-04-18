using CoNet.Services.Server;
using Microsoft.AspNetCore.Mvc;

namespace CoNET.Test.Web.Controllers;

[Route("/[controller]")]
public class ServerController : ControllerBase
{

    public ServerService ServerService { get; }

    [Route("{**path}")]
    public IActionResult Get([FromRoute] string path)
    {
        path ??= "";
        var configuration = ServerService.GetConfiguration($"server/{path}");
        return Ok(new
        {
            path,
            value = configuration?["Key"]
        });
    }

    public ServerController(ServerService serverService)
    {
        ServerService = serverService;
    }

}