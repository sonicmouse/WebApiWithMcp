using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using WebApiWithMcp.Models;
using WebApiWithMcp.Services;

namespace WebApiWithMcp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public sealed class WeatherForecastController(IWeatherService weatherService) : ControllerBase
	{
		private readonly IWeatherService _weatherService = weatherService;

		[HttpGet]
		[Description("Retrieves the weather forecast for a specific zipcode.")]
		public IEnumerable<WeatherForecast> Get([FromQuery] string zipCode)
		{
			return _weatherService.GetForecastAsync(zipCode);
		}
	}
}
