using Microsoft.AspNetCore.Mvc;
using ModelContextProtocol.Server;
using System.ComponentModel;
using WebApiWithMcp.Models;
using WebApiWithMcp.Services;

namespace WebApiWithMcp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[McpServerToolType]
	public class WeatherForecastController : ControllerBase
	{
		public WeatherForecastController(IWeatherService weatherService)
		{
			_weatherService = weatherService;
		}

		private readonly IWeatherService _weatherService;

		[HttpGet(Name = "GetWeatherForecast")]
		[McpServerTool(Name = "get_weather_forecast")]
		[Description("Retrieves the weather forecast for a specific location.")]
		public IEnumerable<WeatherForecast> Get([FromQuery] string zipCode)
		{
			return _weatherService.GetForecastAsync(zipCode);
		}
	}
}
