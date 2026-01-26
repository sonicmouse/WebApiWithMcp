using ModelContextProtocol.Server;
using System.ComponentModel;
using WebApiWithMcp.Models;

namespace WebApiWithMcp.Services
{
	public interface IWeatherService
	{
		IReadOnlyList<WeatherForecast> GetForecastAsync(string zipCode);
	}

	[McpServerToolType]
	internal sealed class WeatherService : IWeatherService
	{
		private static readonly string[] _summaries =
		[
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		];

		[McpServerTool(Name = "get_weather_forecast")]
		[Description("Retrieves the weather forecast for a specific zipcode.")]
		public IReadOnlyList<WeatherForecast> GetForecastAsync(string zipCode)
		{
			return [.. Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = _summaries[Random.Shared.Next(_summaries.Length)]
			})];
		}
	}
}
