using WebApiWithMcp.Models;

namespace WebApiWithMcp.Services
{
	public interface IWeatherService
	{
		IReadOnlyList<WeatherForecast> GetForecastByZipCode(string zipCode);
	}

	internal sealed class WeatherService : IWeatherService
	{
		private static readonly string[] _summaries =
		[
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		];

		public IReadOnlyList<WeatherForecast> GetForecastByZipCode(string zipCode)
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
