using System.Collections.Generic;
using System.Threading.Tasks;
using LogCollector.Domain;

namespace LogCollector.Clients
{
    public interface ILogCollectorClient
    {
        Task<IEnumerable<Event>> GetNumberOfLogsWithKeyword(string machine, string filename, int numberOfEvents = 250, string keyword = " ");
    }
}