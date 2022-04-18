using System;

namespace LogCollector.Domain
{
    public class Event
    {
        public string timestamp { get; set; }
        public string message { get; set; }
    }
}