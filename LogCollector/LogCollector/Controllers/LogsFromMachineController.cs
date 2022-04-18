using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogCollector.Clients;
using LogCollector.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LogCollector.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class LogsFromMachineController : ControllerBase
    {
        private readonly ILogCollectorClient _logCollectorClient;
        private ILogger<LogsFromMachineController> _logger;
        private const int DefaultNumberOfEvents = 250;

        public LogsFromMachineController(ILogCollectorClient logCollectorClient,
            ILogger<LogsFromMachineController> logger)
        {
            _logCollectorClient = logCollectorClient;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("GetLogsFromAllMachines")]
        public async Task<ActionResult> GetLogsFromMachines(string machines, string filename)
        {
            var tasks = new List<Task<MachineEvents>>();
            foreach (var machine in machines.Split("%20"))
            {
                tasks.Add(_logCollectorClient.GetNumberOfLogsWithKeyword(machine, filename));
            }

            var completedTasks = await Task.WhenAll(tasks);
            var machineEvents = completedTasks.ToList();
            return Ok(machineEvents);
        }
        
        [HttpGet]
        [Route("GetNumberOfLogsFromAllMachines")]
        public async Task<ActionResult> GetLogsFromMachines(string machines, string filename, int numberOfEvents)
        {
            var tasks = new List<Task<MachineEvents>>();
            foreach (var machine in machines.Split("%20"))
            {
                tasks.Add(_logCollectorClient.GetNumberOfLogsWithKeyword(machine, filename, numberOfEvents));
            }

            var completedTasks = await Task.WhenAll(tasks);
            var machineEvents = completedTasks.ToList();
            return Ok(machineEvents);
        }
        
        [HttpGet]
        [Route("GetLogsWithKeywordFromAllMachines")]
        public async Task<ActionResult> GetLogsFromMachines(string machines, string filename, string keyword)
        {
            var tasks = new List<Task<MachineEvents>>();
            foreach (var machine in machines.Split("%20"))
            {
                tasks.Add(_logCollectorClient.GetNumberOfLogsWithKeyword(machine, filename, DefaultNumberOfEvents, keyword));
            }

            var completedTasks = await Task.WhenAll(tasks);
            var machineEvents = completedTasks.ToList();
            return Ok(machineEvents);
        }
        
        [HttpGet]
        [Route("GetNumberOfLogsWithKeywordFromAllMachines")]
        public async Task<ActionResult> GetLogsFromMachines(string machines, string filename, int numberOfEvents, string keyword)
        {
            var tasks = new List<Task<MachineEvents>>();
            foreach (var machine in machines.Split("%20"))
            {
                tasks.Add(_logCollectorClient.GetNumberOfLogsWithKeyword(machine, filename, numberOfEvents, keyword));
            }

            var completedTasks = await Task.WhenAll(tasks);
            var machineEvents = completedTasks.ToList();
            return Ok(machineEvents);
        }
    }
}