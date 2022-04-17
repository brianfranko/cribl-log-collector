using System;

namespace LogCollector.Configuration
{
    public class LogCollectorConfiguration
    {
        public const string LogCollector = "LogCollector";
        
        public string LogsLocation { get; set; } = String.Empty;
    }
}