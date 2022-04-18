using System;

namespace LogCollector.Configuration
{
    public class LogCollectorConfiguration
    {
        // Section name in the appsettings
        public const string LogCollector = "LogCollector";
        
        // Path to logs.
        public string LogsLocation { get; set; } = String.Empty;
    }
}