using WeatherApi.Models;

namespace WeatherApi.Services;

public interface IWeatherService
{
    public IEnumerable<SearchLocation> FetchLocation(string query, string lang = "en");

    public Weather FetchWeatherForecast(string query, int days = 3, string lang = "en");
}
