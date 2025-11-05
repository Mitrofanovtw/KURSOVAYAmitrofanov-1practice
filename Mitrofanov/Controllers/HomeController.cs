using Microsoft.AspNetCore.Mvc;

namespace StudioStatistic.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                message = "API студии звукозаписи",
                version = "1.0",
                endpoints = new[]
                {
                    "GET /api/clients",
                    "GET /api/clients/{id}",
                    "POST /api/clients",
                    "Swagger: /swagger"
                },
                timestamp = DateTime.Now
            });
        }
    }
}