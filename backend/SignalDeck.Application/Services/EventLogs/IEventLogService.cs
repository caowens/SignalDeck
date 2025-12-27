using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.EventLog;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.Services.EventLogs
{
    public interface IEventLogService
    {
        Task<EventLogDto> CreateAsync(CreateEventLogRequest request);
        Task<IEnumerable<EventLogDto>> GetByApplicationIdAsync(int appId);
        Task<IEnumerable<EventLogDto>> GetLogsBySeverityAsync(int appId, EventLogSeverity severity);
    }
}