using System.Collections.Generic;
using LogCollector.Domain;

namespace LogCollector.Services
{
    public interface IEventFilteringService
    {
        IEnumerable<Event> FilterNumberOfEvents(IEnumerable<Event> events, int numberOfEvents);
    }
}