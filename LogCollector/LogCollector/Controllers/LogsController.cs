using System.IO;
using System.Linq;
using LogCollector.Configuration;
using LogCollector.Services;
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
        private readonly IEventReadingService _eventReadingService;
        private readonly IEventFilteringService _eventFilteringService;
        private const int DefaultNumberOfEvents = 250;

        public LogsController(ILogger<LogsController> logger, IEventReadingService eventReadingService, IEventFilteringService eventFilteringService)
        {
            _logger = logger;
            _eventReadingService = eventReadingService;
            _eventFilteringService = eventFilteringService;
        }
        
        [HttpGet]
        [Route("GetLogsByFilename")]
        public IActionResult GetLogs(string filename)
        {
            try
            {
                if (_eventReadingService.FileExists(filename))
                {
                    var results = _eventReadingService.ReadEventsFromFile(filename);
                    return results.Any() 
                        ? Ok(_eventFilteringService.FilterNumberOfEvents(results, DefaultNumberOfEvents)) 
                        : Ok("Log does not contain any events.");
                }
                return Ok("File does not exist.");
            }
            catch (IOException e)
            {
                _logger.LogError("Error reading log files");
                return BadRequest("Error reading log files");
            }
        }
        
        [HttpGet]
        [Route("GetNumberOfLogsByFilename")]
        public IActionResult GetLogs(string filename, int numberOfEvents)
        {
            try
            {
                if (!_eventReadingService.FileExists(filename)) return Ok("File does not exist.");
                var results = _eventReadingService.ReadEventsFromFile(filename);
                return !results.Any() ? 
                    Ok("Log does not contain any events.") : Ok(_eventFilteringService.FilterNumberOfEvents(results, numberOfEvents));
            }
            catch (IOException e)
            {
                _logger.LogError("Error reading log files");
                return BadRequest("Error reading log files");
            }
        }

    }
}