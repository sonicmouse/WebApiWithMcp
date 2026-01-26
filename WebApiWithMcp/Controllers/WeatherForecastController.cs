using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using WebApiWithMcp.Models;
using WebApiWithMcp.Services;

namespace WebApiWithMcp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		public WeatherForecastController(IWeatherService weatherService)
		{
			_weatherService = weatherService;
		}

		private readonly IWeatherService _weatherService;

		[HttpGet]
		[Description("Retrieves the weather forecast for a specific zip code.")]
		public IEnumerable<WeatherForecast> Get([FromQuery] string zipCode)
		{
			return _weatherService.GetForecastAsync(zipCode);
		}
	}
}
