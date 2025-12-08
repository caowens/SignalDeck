using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SignalDeck.Application.Interfaces;
using SignalDeck.Domain.Entities;
using SignalDeck.Infrastructure.Data;

namespace SignalDeck.Infrastructure.Repositories
{
    public class EventLogRepository : IEventLogRepository
    {
        private readonly SignalDeckDbContext _context;
        public EventLogRepository(SignalDeckDbContext context)
        {
            _context = context;
        }

        public async Task<EventLog> AddAsync(EventLog eventLog)
        {
            await _context.EventLogs.AddAsync(eventLog);
            await _context.SaveChangesAsync();
            return eventLog;
        }

        public async Task<IEnumerable<EventLog>> GetByApplicationIdAsync(int appId)
        {
            return await _context.EventLogs
                .Where(e => e.ApplicationId == appId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<EventLog>> GetLogsBySeverityAsync(int appId, EventLogSeverity severity)
        {
            return await _context.EventLogs
                .Where(e => e.ApplicationId == appId && e.Severity == severity)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}