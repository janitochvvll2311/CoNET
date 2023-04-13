using CoNET.Services.Rooms;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton(new RoomService(128));

var app = builder.Build();
app.UseFileServer();
app.UseWebSockets();

app.Run();
