using System.Collections.Generic;

namespace LogCollector.Domain
{
    public class MachineEvents
    {
        public string machine { get; set; }
        public IEnumerable<Event> events { get; set; }
        public string message { get; set; }

        public MachineEvents()
        {
            events = new List<Event>();
        }
    }
}