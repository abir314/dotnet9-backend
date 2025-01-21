using Microsoft.AspNetCore.Mvc;

namespace hello_api.Api;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    private readonly HttpClient _client;

    public WeatherController(ILogger<WeatherController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _client = httpClientFactory.CreateClient();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try 
        {
            _logger.LogInformation("Weather endpoint called at {Time}", DateTime.UtcNow);
            
            await Task.Delay(100);
            
            var forecast = new
            {
                Date = DateTime.UtcNow,
                Temperature = 25,
                Summary = "Sunny",
                RequestId = Guid.NewGuid()
            };
            
            return Ok(forecast);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting weather forecast");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
}