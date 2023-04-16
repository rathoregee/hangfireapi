using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace g2v.core.clinetsync.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        private readonly IBackgroundJobClient _jobClient;       

        private readonly ILogger<HealthCheckController> _logger;

        public HealthCheckController(ILogger<HealthCheckController> logger, IBackgroundJobClient jobClient)
        {
            _logger = logger;
            _jobClient = jobClient;
        }
        [HttpGet(Name = "")]
        public ActionResult Get()
        {
            _jobClient.Enqueue(() => Console.WriteLine("Health check job"));
            _logger.LogInformation("Health check endpoint is working");
            return Ok("Health check endpoint is working");
        }
    }
}