using System.Collections.Generic;
using LogCollector.Domain;

namespace LogCollector.Services
{
    public interface IEventReadingService
    {
        IEnumerable<Event> ReadEventsWithKeywordFromFile(string file, string keyword);

        bool FileExists(string filename);
    }
}