using Microsoft.AspNetCore.Authentication;
using WebApiWithMcp.Authentication;
using WebApiWithMcp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IWeatherService, WeatherService>();

builder.Services.AddAuthentication("ApiKey")
	.AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", null);

builder.Services.AddAuthorization();

builder.Services.AddMcpServer()
	.WithHttpTransport()
	.WithToolsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapMcp("/mcp").RequireAuthorization();

app.MapControllers();
app.Run();
