using CoNet.Services.Server;
using CoNET.Services.Rooms;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton(new RoomService(128));
builder.Services.AddSingleton(new ServerService(builder.Environment.WebRootPath));

var app = builder.Build();
app.UseFileServer();
app.UseWebSockets();
app.MapControllers();

app.Run();
