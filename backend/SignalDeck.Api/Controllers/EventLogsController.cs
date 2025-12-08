using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalDeck.Application.DTOs.EventLog;
using SignalDeck.Application.Services;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventLogsController : ControllerBase
    {
        private readonly EventLogService _eventLogService;
        public EventLogsController(EventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }

        [HttpGet("{appId}")]
        public async Task<IActionResult> GetByApplicationId(int appId)
        {
            var eventLogs = await _eventLogService.GetByApplicationIdAsync(appId);
            return Ok(eventLogs);
        }

        [HttpGet("{appId}/severity/{severity}")]
        public async Task<IActionResult> GetLogsBySeverity(int appId, EventLogSeverity severity)
        {
            var eventLogs = await _eventLogService.GetLogsBySeverityAsync(appId, severity);
            return Ok(eventLogs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventLogRequest request)
        {
            var eventLog = await _eventLogService.CreateAsync(request);
            return CreatedAtAction(nameof(GetByApplicationId), new { appId = eventLog.ApplicationId }, eventLog);
        }
    }
}