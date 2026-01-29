using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using WebApiWithMcp.Infrastructure;
using WebApiWithMcp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IWeatherService, WeatherService>();

// NEW: Add MCP server with HTTP transport and tools from the current assembly
builder.Services.AddMcpServer()
	.WithHttpTransport()
	.WithToolsFromAssembly();

// NEW (Optional): Add API key authentication scheme
builder.Services.AddAuthentication(ApiKeyAuthenticationHandler.SchemeName)
	.AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationHandler.SchemeName, null);
builder.Services.AddAuthorization();

// ----------------------------------------------------------
var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// NEW: Map MCP endpoint at /mcp. Optionally require authorization/authentication
app.MapMcp("/mcp")
	.RequireAuthorization(new AuthorizeAttribute { AuthenticationSchemes = ApiKeyAuthenticationHandler.SchemeName });

app.MapControllers();
await app.RunAsync();
