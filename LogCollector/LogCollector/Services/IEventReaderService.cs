using System.Collections.Generic;
using LogCollector.Domain;

namespace LogCollector.Services
{
    public interface IEventReaderService
    {
        IEnumerable<Event> ReadEventsWithKeywordFromFile(string path, string keyword);

        bool FileExists(string filename, out string path);
    }
}