using System.IO;
using System.Linq;
using LogCollector.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LogCollector.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly ILogger<LogsController> _logger;
        private readonly IEventReaderService _eventReaderService;
        private readonly IEventFilterService _eventFilterService;
        private const int DefaultNumberOfEvents = 250;

        public LogsController(ILogger<LogsController> logger, IEventReaderService eventReaderService, IEventFilterService eventFilterService)
        {
            _logger = logger;
            _eventReaderService = eventReaderService;
            _eventFilterService = eventFilterService;
        }
        
        [HttpGet]
        [Route("GetLogsByFilename")]
        public IActionResult GetLogs(string filename)
        {
            try
            {
                if (!_eventReaderService.FileExists(filename)) return Ok("File does not exist.");
                // Use a space to return all results
                var results = _eventReaderService.ReadEventsWithKeywordFromFile(filename, " ");
                return results.Any() 
                    ? Ok(_eventFilterService.FilterNumberOfEvents(results, DefaultNumberOfEvents)) 
                    : Ok("Log does not contain any events.");
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
                if (!_eventReaderService.FileExists(filename)) return Ok("File does not exist.");
                // Use a space to return all results
                var results = _eventReaderService.ReadEventsWithKeywordFromFile(filename, " ");
                return !results.Any() ? 
                    Ok("Log does not contain any events.") : Ok(_eventFilterService.FilterNumberOfEvents(results, numberOfEvents));
            }
            catch (IOException e)
            {
                _logger.LogError("Error reading log files");
                return BadRequest("Error reading log files");
            }
        }
        
        [HttpGet]
        [Route("GetLogsWithKeywordByFilename")]
        public IActionResult GetLogs(string filename, string keyword)
        {
            try
            {
                if (!_eventReaderService.FileExists(filename)) return Ok("File does not exist.");
                var results = _eventReaderService.ReadEventsWithKeywordFromFile(filename, keyword);
                return !results.Any() ? 
                    Ok("Log does not contain any events for that keyword.") : Ok(_eventFilterService.FilterNumberOfEvents(results, DefaultNumberOfEvents));
            }
            catch (IOException e)
            {
                _logger.LogError("Error reading log files");
                return BadRequest("Error reading log files");
            }
        }
        
        [HttpGet]
        [Route("GetNumberOfLogsWithKeywordByFilename")]
        public IActionResult GetLogs(string filename, int numberOfEvents, string keyword)
        {
            try
            {
                if (!_eventReaderService.FileExists(filename)) return Ok("File does not exist.");
                var results = _eventReaderService.ReadEventsWithKeywordFromFile(filename, keyword);
                return !results.Any() ? 
                    Ok("Log does not contain any events for that keyword.") : Ok(_eventFilterService.FilterNumberOfEvents(results, numberOfEvents));
            }
            catch (IOException e)
            {
                _logger.LogError("Error reading log files");
                return BadRequest("Error reading log files");
            }
        }

    }
}