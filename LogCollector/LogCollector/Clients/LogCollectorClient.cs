using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using LogCollector.Domain;
using Microsoft.Extensions.Logging;

namespace LogCollector.Clients
{
    public class LogCollectorClient : ILogCollectorClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LogCollectorClient> _logger;

        public LogCollectorClient(HttpClient httpClient, ILogger<LogCollectorClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<MachineEvents> GetNumberOfLogsWithKeyword(string machine, string filename, int numberOfEvents = 250, string keyword = " ")
        {
            try
            {
                var uri = BuildUri(machine, filename);
                // TODO: Add in rety and timeout handlers for HTTP call
                var response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                try
                {
                    var events = JsonSerializer.Deserialize<List<Event>>(responseBody);
                    return events == null
                        ? new MachineEvents()
                        {
                            machine = machine,
                            message = "Failed to get logs from machine."
                        }
                        : new MachineEvents()
                        {
                            machine = machine,
                            events = events
                        };
                }
                // TODO: Update the response code when the file does not exist to handle this better.
                catch (JsonException je)
                {
                    // If the file does not exist we will return 200 but we will not be able to parse the list of events.
                    return new MachineEvents()
                    {
                        machine = machine,
                        message = responseBody
                    };
                }
                
                
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message);
                return new MachineEvents()
                {
                    machine = machine,
                    message = e.Message
                };
            }
        }

        private Uri BuildUri(string machine, string filename, int numberOfEvents = 250, string keyword = "")
        {
            return new Uri($"http://{machine}/api/v1/Logs/GetNumberOfLogsWithKeywordByFilename?{filename}&numberOfevents={numberOfEvents}&keyword={keyword}");
            
        }
    }
}