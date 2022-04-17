using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using LogCollector.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LogCollector.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly ILogger<LogsController> _logger;
        private readonly LogCollectorConfiguration _configuration;

        public LogsController(ILogger<LogsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = new LogCollectorConfiguration();
            configuration.GetSection(LogCollectorConfiguration.LogCollector).Bind(_configuration);
        }
        
        [HttpGet]
        [Route("filename/{filename}")]
        public async Task<ActionResult> GetLogs(string filename)
        {
            try
            {
                filename = filename + ".log";
                var path = Path.Combine(_configuration.LogsLocation, filename);
                if (System.IO.File.Exists(path))
                {
                    var events = System.IO.File.ReadLines(path);
                    return await Task.FromResult(Ok(events));
                }

                return await Task.FromResult(Ok("file does not exist"));
            }
            catch (IOException e)
            {
                _logger.LogError("Error reading log files");
                return BadRequest("Error reading log files");
            }
            
        }

    }
}