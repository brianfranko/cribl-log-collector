using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LogCollector.Configuration;
using LogCollector.Domain;
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
                    var results = new List<Event>();
                    foreach (var log in events.Reverse())
                    {
                        var logEvent = new Event();
                        logEvent.timestamp = log.Substring(0, 15).Trim();
                        logEvent.Message = log.Substring(15).Trim();
                        results.Add(logEvent);
                    }
                    return await Task.FromResult(Ok(results));
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