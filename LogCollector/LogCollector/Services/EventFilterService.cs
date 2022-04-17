using System.Collections.Generic;
using System.Linq;
using LogCollector.Domain;

namespace LogCollector.Services
{
    public class EventFilterService : IEventFilterService
    {
        public IEnumerable<Event> FilterNumberOfEvents(IEnumerable<Event> events, int numberOfEvents)
        {
            return events.Take(numberOfEvents);
        }
    }
}