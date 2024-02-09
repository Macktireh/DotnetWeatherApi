using Microsoft.AspNetCore.Mvc;
using WeatherApi.Models;
using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("api/")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherService _weatherService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    [HttpGet("search")]
    public IEnumerable<SearchLocation> GetSearchLocations(string? q, string? lang = "en")
    {
        if (string.IsNullOrEmpty(q))
        {
            return [];
        }
        return _weatherService.FetchLocation(query: q, lang: lang!);
    }

    [HttpGet("forecast")]
    public Weather GetWeatherForecasts(string? q, int days = 3, string? lang = "en")
    {
        if (string.IsNullOrEmpty(q))
        {
            return new Weather();
        }
        return _weatherService.FetchWeatherForecast(query: q, days: days, lang: lang!);
    }
}
