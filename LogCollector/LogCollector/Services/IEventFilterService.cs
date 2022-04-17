using System.Collections.Generic;
using LogCollector.Domain;

namespace LogCollector.Services
{
    public interface IEventFilterService
    {
        IEnumerable<Event> FilterNumberOfEvents(IEnumerable<Event> events, int numberOfEvents);
    }
}