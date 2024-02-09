using WeatherApi.Models;

namespace WeatherApi.Services;

public class WeatherService : IWeatherService
{
    private static readonly string? WEATHER_API_URL = Environment.GetEnvironmentVariable("WEATHER_API_URL");
    private static readonly string? WEATHER_API_KEY = Environment.GetEnvironmentVariable("WEATHER_API_KEY");
    private static readonly HttpClient _httpClient = new HttpClient { };

    public IEnumerable<SearchLocation> FetchLocation(string query, string lang = "en")
    {
        ValidateWeatherApiSettings();

        string apiUrl = $"{WEATHER_API_URL}/search.json?key={WEATHER_API_KEY}&q={query}&lang={lang}";

        HttpResponseMessage response = _httpClient.GetAsync(apiUrl).Result;

        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadFromJsonAsync<IEnumerable<SearchLocation>>().Result;
            return result!;
        }

        throw new HttpRequestException($"Weather API request failed with status code {response.StatusCode}");
    }

    public Weather FetchWeatherForecast(string query, int days = 3, string lang = "en")
    {
        ValidateWeatherApiSettings();

        string apiUrl = $"{WEATHER_API_URL}/forecast.json?key={WEATHER_API_KEY}&q={query}&days={days}&lang={lang}";

        HttpResponseMessage response = _httpClient.GetAsync(apiUrl).Result;

        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadFromJsonAsync<Weather>().Result;
            return result!;
        }

        throw new HttpRequestException($"Weather API request failed with status code {response.StatusCode}");
    }

    private void ValidateWeatherApiSettings()
    {
        if (string.IsNullOrEmpty(WEATHER_API_URL) || string.IsNullOrEmpty(WEATHER_API_KEY))
        {
            throw new Exception("WEATHER_API_URL and WEATHER_API_KEY must be set in the .env file.");
        }
    }
}
