// using Microsoft.AspNetCore.Mvc;
// using Voyager.Api.Services;
// using Voyager.Api.Views;

// namespace Voyager.Api.Controllers;

// [ApiController]
// [Route("api/v1/[controller]")]
// public class CodingResourcesController : ControllerBase
// {
//     private static readonly string[] Summaries = new[]
//     {
//         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//     };

//     private readonly ILogger<CodingResourcesController> _logger;
//     private readonly ICodingResourceService _codingResourceService;

//     public CodingResourcesController(ICodingResourceService codingResourceService, ILogger<CodingResourcesController> logger)
//     {
//         _codingResourceService = codingResourceService;
//         _logger = logger;
//     }

//     [HttpGet(Name = "GetCodingResource")]
//     public IEnumerable<WeatherForecast> Get()
//     {
//         try
//         {
//             var startTime = DateTime.Now;
//             _logger.LogInformation("Fetching random codingResource at timeStamp : {@TimeStamp}", startTime);
//             var codingResource = await _codingResourceService.GetCodingResourceAsync();
//             var endTime = DateTime.Now;
//             _logger.LogInformation("Successfully fetched the random codingResource at timeStamp : {@TimeStamp} and TimeElasped:{@TimeElapsed}", endTime, endTime - startTime);
//             return Ok(codingResource);
//         }
//         catch (Exception ex)
//         {
//             _logger.LogError(ex, "Error occurred while fetching random codingResource");
//             return StatusCode(500, ex.Message);
//         }
//     }
// }
