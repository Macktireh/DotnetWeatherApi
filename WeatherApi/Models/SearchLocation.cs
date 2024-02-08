namespace WeatherApi.Models;

public class SearchLocation
{

    public int id { get; set; }
    public string name { get; set; }
    public string region { get; set; }
    public string country { get; set; }
    public double lat { get; set; }
    public double lon { get; set; }
    public string url { get; set; }

}
