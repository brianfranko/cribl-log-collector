using System.Collections.Generic;
using System.Linq;
using LogCollector.Configuration;
using LogCollector.Domain;
using LogCollector.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace LogCollector.UnitTests.Services
{
    [TestFixture]
    public class EventReaderServiceTests
    {
        private LogCollectorConfiguration _configuration;
        private Mock<ILogger<EventReaderService>> _mockLogger;

        [SetUp]
        public void setup()
        {
            _configuration = new LogCollectorConfiguration()
            {
                LogsLocation = "./logs"
            };
            _mockLogger = new Mock<ILogger<EventReaderService>>();
        }

        [Test]
        public void EventReaderServiceFiltersByKeyWordAcurrarely()
        {
            var expected = new List<Event>()
            {
                new Event()
                {
                    timestamp = "Jun 17 20:55:06",
                    message = "combo ftpd[30755]: connection from 82.252.162.81 (lns-vlq-45-tou-82-252-162-81.adsl.proxad.net) at Fri Jun 17 20:55:06 2005"
                }
            };
            var eventReaderService = new EventReaderService(_configuration, _mockLogger.Object);
            var actual = eventReaderService.ReadEventsWithKeywordFromFile("./logs/Connection.log", "30755");
            Assert.AreEqual(expected.Count, actual.Count());
            Assert.AreEqual(expected.First().message, actual.First().message);
        }
        
        [Test]
        public void EventReaderServiceAddsAllEventsWhenASpaceIsTheKeyword()
        {
            var expected = new List<Event>()
            {
                new Event()
                {
                    timestamp = "Jun 17 20:55:06",
                    message = "combo ftpd[30759]: connection from 82.252.162.81 (lns-vlq-45-tou-82-252-162-81.adsl.proxad.net) at Fri Jun 17 20:55:07 2005"
                }
            };
            var eventReaderService = new EventReaderService(_configuration, _mockLogger.Object);
            var actual = eventReaderService.ReadEventsWithKeywordFromFile("./logs/Connection.log", " ");
            Assert.AreEqual(expected.First().message, actual.First().message);
        }

        [Test]
        public void EventReaderServiceReturnsFalseWhenTheFileDoesNotExist()
        {
            var eventReaderService = new EventReaderService(_configuration, _mockLogger.Object);
            var actual = eventReaderService.FileExists("./logs/NonExistentFile.log", out string path);
            Assert.IsFalse(actual);
        }
        
        [Test]
        public void EventReaderServiceReturnsTrueWhenTheFileExists()
        {
            var eventReaderService = new EventReaderService(_configuration, _mockLogger.Object);
            var actual = eventReaderService.FileExists("Connection", out string path);
            Assert.IsTrue(actual);
        }
    }
}