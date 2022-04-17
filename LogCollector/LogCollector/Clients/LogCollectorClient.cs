using System.Collections.Generic;
using System.Net.Http;
using LogCollector.Domain;

namespace LogCollector.Clients
{
    public class LogCollectorClient : ILogCollectorClient
    {
        private readonly HttpClient _httpClient;

        public LogCollectorClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<Event> GetLogs(string machine, string filename)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Event> GetNumberOfEventsFromLogs(string machine, string filename, int numberOfEvents)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Event> GetLogsWithKeyword(string machine, string filename, string keyword)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Event> GetNumberOfLogsWithKeyword(string machine, string filename, int numberOfEvents, string keyword)
        {
            throw new System.NotImplementedException();
        }
    }
}