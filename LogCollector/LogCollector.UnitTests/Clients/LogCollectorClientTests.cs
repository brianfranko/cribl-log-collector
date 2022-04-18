using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LogCollector.Clients;
using LogCollector.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace LogCollectorTests.Clients
{
    [TestFixture]
    public class LogCollectorClientTests
    {
        private HttpClient _httpClient;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private Mock<ILogger<LogCollectorClient>> _logger;

        [SetUp]
        public void setup()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _logger = new Mock<ILogger<LogCollectorClient>>();
        }

        [Test]
        public async Task LogCollectorClientSerializesEventsCorrectly()
        {
            var jsonString = "[" +
                             "{ " +
                             "\"timestamp\": \"Jun 17 20:55:07\"," +
                             "\"message\": \"combo ftpd[30759]: connection from 82.252.162.81 (lns-vlq-45-tou-82-252-162-81.adsl.proxad.net) at Fri Jun 17 20:55:07 2005\"" +
                             "}" +
                             "]";
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString)
                });
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            var expected = new List<Event>()
            {
                new Event()
                {
                    timestamp = "Jun 17 20:55:07",
                    message =
                        "combo ftpd[30759]: connection from 82.252.162.81 (lns-vlq-45-tou-82-252-162-81.adsl.proxad.net) at Fri Jun 17 20:55:07 2005"
                }
            };
            var client = new LogCollectorClient(_httpClient, _logger.Object);
            var actual = await client.GetNumberOfLogsWithKeyword("machine", "file");
            Assert.AreEqual(expected.First().timestamp, actual.First().timestamp);
            Assert.AreEqual(expected.First().message, actual.First().message);
        }

        [Test]
        public async Task WhenAnErrorIsThrownAnEmptyListIsReturned()
        {
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException());
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            var expected = new List<Event>();
            var client = new LogCollectorClient(_httpClient, _logger.Object);
            var actual = await client.GetNumberOfLogsWithKeyword("machine", "file");
            Assert.AreEqual(expected.Count, actual.Count());
        }

        [Test]

        public async Task WhenResponseCodeIsNot200AnEmptyListIsReturned()
        {
            var jsonString = "[" +
                             "{ " +
                             "\"timestamp\": \"Jun 17 20:55:07\"," +
                             "\"message\": \"combo ftpd[30759]: connection from 82.252.162.81 (lns-vlq-45-tou-82-252-162-81.adsl.proxad.net) at Fri Jun 17 20:55:07 2005\"" +
                             "}" +
                             "]";
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage {
                    StatusCode = HttpStatusCode.Forbidden,
                    Content = new StringContent(jsonString)
                });
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            var expected = new List<Event>();
            var client = new LogCollectorClient(_httpClient, _logger.Object);
            var actual = await client.GetNumberOfLogsWithKeyword("machine", "file");
            Assert.AreEqual(expected.Count, actual.Count());
        }
    }
}