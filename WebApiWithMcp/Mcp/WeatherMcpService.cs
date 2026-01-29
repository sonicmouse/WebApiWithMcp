using Microsoft.AspNetCore.Mvc;
using ModelContextProtocol.Server;
using System.ComponentModel;
using WebApiWithMcp.Models;
using WebApiWithMcp.Services;

namespace WebApiWithMcp.Mcp
{
	[McpServerToolType]
	internal static class WeatherMcpService
	{
		[McpServerTool(Title = "Weather Forecast by Zip Code", Name = "get_weather_forecast", ReadOnly = true)]
		[Description("Retrieves the weather forecast for a specific zipcode.")]
		public static IReadOnlyList<WeatherForecast> GetForecastByZipCode(
			[Description("The 5-digit US postal zip code for the location")] string zipCode,
			[FromServices] IWeatherService weatherService)
		{
			return weatherService.GetForecastByZipCode(zipCode);
		}
	}
}
