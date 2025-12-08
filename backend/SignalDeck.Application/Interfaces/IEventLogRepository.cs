using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.Interfaces
{
    public interface IEventLogRepository
    {
        Task<IEnumerable<EventLog>> GetByApplicationIdAsync(int appId);
        Task<IEnumerable<EventLog>> GetLogsBySeverityAsync(int appId, EventLogSeverity severity);
        Task<EventLog> AddAsync(EventLog eventLog);

    }
}