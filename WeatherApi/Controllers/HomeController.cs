using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public RedirectResult Swagger()
    {
        return Redirect("/swagger/index.html");
    }
}
