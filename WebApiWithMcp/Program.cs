using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using WebApiWithMcp.Infrastructure;
using WebApiWithMcp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IWeatherService, WeatherService>();

// NEW (Optional): Add API key authentication scheme
builder.Services.AddAuthentication("ApiKey")
	.AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", null);

builder.Services.AddAuthorization();

// NEW: Add MCP server with HTTP transport and tools from the current assembly
builder.Services.AddMcpServer()
	.WithHttpTransport()
	.WithToolsFromAssembly(typeof(Program).Assembly);

// ----------------------------------------------------------
var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// NEW: Map MCP endpoint at /mcp. Optionally require API key authentication
app.MapMcp("/mcp")
	.RequireAuthorization(new AuthorizeAttribute { AuthenticationSchemes = "ApiKey" });

app.MapControllers();
await app.RunAsync();
