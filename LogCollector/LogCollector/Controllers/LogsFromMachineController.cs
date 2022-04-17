using System;
using System.Collections.Generic;
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

        public LogsFromMachineController(ILogCollectorClient logCollectorClient,
            ILogger<LogsFromMachineController> logger)
        {
            _logCollectorClient = logCollectorClient;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("GetLogsFromAllMachines")]
        public ActionResult GetLogsFromMachines(string machines, string filename)
        {
            var machineEvents = new List<MachineEvents>();
            foreach (var machine in machines.Split("%20"))
            {
                var machineEvent = new MachineEvents()
                {
                    machine = machine
                };
                try
                {
                    var events = _logCollectorClient.GetLogs(machine, filename);
                    machineEvent.events = events;
                    machineEvents.Add(machineEvent);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Failed to get logs from machine: {machine}");
                    machineEvent.message = e.Message;
                    machineEvents.Add(machineEvent);
                }
            }
            return Ok(machineEvents);
        }
        
        [HttpGet]
        [Route("GetNumberOfLogsFromAllMachines")]
        public ActionResult GetLogsFromMachines(string machines, string filename, int numberOfEvents)
        {
            var machineEvents = new List<MachineEvents>();
            foreach (var machine in machines.Split("%20"))
            {
                var machineEvent = new MachineEvents()
                {
                    machine = machine
                };
                try
                {
                    var events = _logCollectorClient.GetNumberOfEventsFromLogs(machine, filename, numberOfEvents);
                    machineEvent.events = events;
                    machineEvents.Add(machineEvent);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Failed to get logs from machine: {machine}");
                    machineEvent.message = e.Message;
                    machineEvents.Add(machineEvent);
                }
            }
            return Ok(machineEvents);
        }
        
        [HttpGet]
        [Route("GetLogsWithKeywordFromAllMachines")]
        public ActionResult GetLogsFromMachines(string machines, string filename, string keyword)
        {
            var machineEvents = new List<MachineEvents>();
            foreach (var machine in machines.Split("%20"))
            {
                var machineEvent = new MachineEvents()
                {
                    machine = machine
                };
                try
                {
                    var events = _logCollectorClient.GetLogsWithKeyword(machine, filename, keyword);
                    machineEvent.events = events;
                    machineEvents.Add(machineEvent);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Failed to get logs from machine: {machine}");
                    machineEvent.message = e.Message;
                    machineEvents.Add(machineEvent);
                }
            }
            return Ok(machineEvents);
        }
        
        [HttpGet]
        [Route("GetNumberOfLogsWithKeywordFromAllMachines")]
        public ActionResult GetLogsFromMachines(string machines, string filename, int numberOfEvents, string keyword)
        {
            var machineEvents = new List<MachineEvents>();
            foreach (var machine in machines.Split("%20"))
            {
                var machineEvent = new MachineEvents()
                {
                    machine = machine
                };
                try
                {
                    var events = _logCollectorClient.GetNumberOfLogsWithKeyword(machine, filename, numberOfEvents, keyword);
                    machineEvent.events = events;
                    machineEvents.Add(machineEvent);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Failed to get logs from machine: {machine}");
                    machineEvent.message = e.Message;
                    machineEvents.Add(machineEvent);
                }
            }
            return Ok(machineEvents);
        }
    }
}