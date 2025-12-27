using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.EventLog;
using SignalDeck.Application.Interfaces;
using SignalDeck.Application.Mapping;
using SignalDeck.Application.Services.EventLogs;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.Services
{
    public class EventLogService : IEventLogService
    {
        private readonly IEventLogRepository _eventLogRepo;
        private readonly IApplicationRepository _appRepo;
        public EventLogService(IEventLogRepository eventLogRepo, IApplicationRepository appRepo)
        {
            _eventLogRepo = eventLogRepo;
            _appRepo = appRepo;
        }

        public async Task<IEnumerable<EventLogDto>> GetByApplicationIdAsync(int appId)
        {
            bool appExists = await _appRepo.ExistsAsync(appId);
            if (!appExists)
            {
                throw new ArgumentException($"Application with ID {appId} does not exist.");
            }

            var eventLogs = await _eventLogRepo.GetByApplicationIdAsync(appId);
            return eventLogs.Select(e => e.ToDto()).ToList();
        }

        public async Task<EventLogDto> CreateAsync(CreateEventLogRequest request)
        {
            bool appExists = await _appRepo.ExistsAsync(request.ApplicationId);
            if (!appExists)
            {
                throw new ArgumentException($"Application with ID {request.ApplicationId} does not exist.");
            }

            if (!IsValidSeverity(request.Severity))
            {
                throw new ArgumentException($"Severity '{request.Severity}' is invalid. Valid values: {string.Join(", ", Enum.GetNames(typeof(EventLogSeverity)))}");
            }

            var eventLog = request.ToEventLogFromCreateRequest();
            await _eventLogRepo.AddAsync(eventLog);
            return eventLog.ToDto();
        }

        public async Task<IEnumerable<EventLogDto>> GetLogsBySeverityAsync(int appId, EventLogSeverity severity)
        {
            bool appExists = await _appRepo.ExistsAsync(appId);
            if (!appExists)
            {
                throw new ArgumentException($"Application with ID {appId} does not exist.");
            }

            var eventLogs = await _eventLogRepo.GetLogsBySeverityAsync(appId, severity);
            return eventLogs.Select(e => e.ToDto()).ToList();
        }

        private bool IsValidSeverity(string severity)
        {
            return Enum.TryParse<EventLogSeverity>(severity, true, out _);
        }
    }
}