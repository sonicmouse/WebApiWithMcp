using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using WebApiWithMcp.Models;
using WebApiWithMcp.Services;

namespace WebApiWithMcp.Controllers
{
	[ApiController, Route("[controller]")]
	public sealed class WeatherForecastController(IWeatherService weatherService) : ControllerBase
	{
		private readonly IWeatherService _weatherService = weatherService;

		[HttpGet]
		[Description("Retrieves the weather forecast for a specific zipcode.")]
		public ActionResult<IEnumerable<WeatherForecast>> GetByZipCode([FromQuery] string zipCode)
		{
			return Ok(_weatherService.GetForecastByZipCode(zipCode));
		}
	}
}
