using System.Threading.Tasks;
using LogCollector.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace LogCollector.UnitTests.Controllers
{
    [TestFixture]
    public class LogControllertests
    {
        private ILogger<LogsController> _logger;
        
        [SetUp]
        public void setup()
        {
            _logger = new Mock<ILogger<LogsController>>().Object;
        }
        
        [Test]
        public async Task logControllerReturnsCorrectFilename()
        {
            var controller = new LogsController(_logger);
            var filename = "Test File";
            var result = (ObjectResult) await controller.GetLogs(filename);
            Assert.AreEqual(filename, result.Value);
        }
    }
}