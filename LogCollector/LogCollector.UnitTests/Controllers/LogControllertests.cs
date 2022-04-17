using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogCollector.Controllers;
using LogCollector.Domain;
using LogCollector.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace LogCollector.UnitTests.Controllers
{
    [TestFixture]
    public class LogControllertests
    {
        private Mock<ILogger<LogsController>> _logger;
        private IEventFilterService _eventFilterService;
        private Mock<IEventReaderService> _eventReaderService;
        
        [SetUp]
        public void setup()
        {
            _logger = new Mock<ILogger<LogsController>>();
            _eventFilterService = new EventFilterService();
            _eventReaderService = new Mock<IEventReaderService>();
        }
        
        [Test]
        public async Task logControllerReturnsFileDoesNotExistCorrectly()
        {
            var controller = new LogsController(_logger.Object, _eventReaderService.Object, _eventFilterService);
            var filename = "Test File";
            var result = (ObjectResult) controller.GetLogs(filename);
            Assert.AreEqual("File does not exist.", result.Value);
        }
        
        [Test]
        public async Task logControllerReturnsFileSuccessfully()
        {
            _eventReaderService.Setup(e => e.FileExists(It.IsAny<string>())).Returns(true);
            var results = new List<Event>()
            {
                new Event
                {
                    Message = "test message",
                    timestamp = "datetime"
                }
            };
            _eventReaderService.Setup(e => e.ReadEventsWithKeywordFromFile(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(results);
            var controller = new LogsController(_logger.Object, _eventReaderService.Object, _eventFilterService);
            var filename = "Connection";
            var result = (ObjectResult) controller.GetLogs(filename);
            Assert.AreEqual(results, result.Value);
        }
        
        [Test]
        public async Task logControllerReturnsTheCorrectNumberOfEventsWhenSpecified()
        {
            _eventReaderService.Setup(e => e.FileExists(It.IsAny<string>())).Returns(true);
            var results = new List<Event>()
            {
                new Event
                {
                    Message = "test message 1",
                    timestamp = "datetime"
                },
                new Event
                {
                    Message = "test message 2",
                    timestamp = "datetime"
                },
                new Event
                {
                    Message = "test message 3",
                    timestamp = "datetime"
                },
                new Event
                {
                    Message = "test message 4",
                    timestamp = "datetime"
                }
            };
            var filteredResults = new List<Event>()
            {
                new Event
                {
                    Message = "test message 1",
                    timestamp = "datetime"
                },
                new Event
                {
                    Message = "test message 2",
                    timestamp = "datetime"
                }
            };
            _eventReaderService.Setup(e => e.ReadEventsWithKeywordFromFile(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(results);
            var controller = new LogsController(_logger.Object, _eventReaderService.Object, _eventFilterService);
            var filename = "Connection";
            var result = (IEnumerable<Event>)((ObjectResult) controller.GetLogs(filename, 2)).Value;
            Assert.AreEqual(filteredResults.Count, result.Count());
        }
    }
}