using System.Collections.Generic;
using LogCollector.Domain;

namespace LogCollector.Clients
{
    public interface ILogCollectorClient
    {
        IEnumerable<Event> GetLogs(string machine, string filename);
        IEnumerable<Event> GetNumberOfEventsFromLogs(string machine, string filename, int numberOfEvents);
        IEnumerable<Event> GetLogsWithKeyword(string machine, string filename, string keyword);
        IEnumerable<Event> GetNumberOfLogsWithKeyword(string machine, string filename, int numberOfEvents, string keyword);
    }
}