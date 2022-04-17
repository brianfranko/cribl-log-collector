using System.Collections.Generic;
using LogCollector.Domain;

namespace LogCollector.Services
{
    public interface IEventReadingService
    {
        IEnumerable<Event> ReadEventsFromFile(string file);

        bool FileExists(string filename);
    }
}