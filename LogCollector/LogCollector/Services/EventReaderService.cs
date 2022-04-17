using System.Collections.Generic;
using System.IO;
using System.Linq;
using LogCollector.Configuration;
using LogCollector.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LogCollector.Services
{
    public class EventReaderService : IEventReaderService
    {
        private readonly LogCollectorConfiguration _configuration;
        private readonly ILogger<EventReaderService> _logger;

        public EventReaderService(LogCollectorConfiguration configuration, ILogger<EventReaderService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IEnumerable<Event> ReadEventsWithKeywordFromFile(string filename, string keyword)
        {
            var results = new List<Event>();
            filename = filename + ".log";
            var path = Path.Combine(_configuration.LogsLocation, filename);
            var events = File.ReadLines(path);
            foreach (var log in events.Reverse())
            {
                var logEvent = new Event();
                logEvent.timestamp = log.Substring(0, 15).Trim();
                logEvent.Message = log.Substring(15).Trim();
                if (logEvent.Message.Contains(keyword)) results.Add(logEvent);
            }
            return results;
        }

        public bool FileExists(string filename)
        {
            filename = filename + ".log";
            var path = Path.Combine(_configuration.LogsLocation, filename);
            return File.Exists(path);
        }
    }
}