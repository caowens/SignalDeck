using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.Persistence
{
    public interface IEventLogRepository
    {
        Task<IEnumerable<EventLog>> GetByApplicationIdAsync(Guid appId);
        Task<IEnumerable<EventLog>> GetLogsBySeverityAsync(Guid appId, EventLogSeverity severity);
        Task<EventLog> AddAsync(EventLog eventLog);

    }
}