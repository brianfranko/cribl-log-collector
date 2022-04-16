using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LogCollector.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly ILogger<LogsController> _logger;

        public LogsController(ILogger<LogsController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        [Route("filename/{filename}")]
        public async Task<ActionResult> GetLogs(string filename)
        {
            return await Task.FromResult(Ok(filename));
        }

    }
}