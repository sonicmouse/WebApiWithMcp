using WebApiWithMcp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IWeatherService, WeatherService>();

builder.Services.AddMcpServer()
	.WithHttpTransport()
	.WithToolsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapMcp("/mcp");

app.MapControllers();
app.Run();
