using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SignalDeck.Application.Persistence;
using SignalDeck.Domain.Entities;
using SignalDeck.Infrastructure.Data;

namespace SignalDeck.Infrastructure.Repositories
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly SignalDeckDbContext _context;
        public ErrorLogRepository(SignalDeckDbContext context)
        {
            _context = context;
        }
        public async Task<ErrorLog> AddAsync(ErrorLog errorLog)
        {
            await _context.ErrorLogs.AddAsync(errorLog);
            await _context.SaveChangesAsync();
            return errorLog;
        }

        public async Task<IEnumerable<ErrorLog>> GetByApplicationIdAsync(Guid appId)
        {
            return await _context.ErrorLogs
                .Where(e => e.ApplicationId == appId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetCountSinceAsync(Guid appId, DateTime since)
        {
            return await _context.ErrorLogs
                .Where(e => e.ApplicationId == appId)
                .Where(e => e.CreatedOn >= since)
                .CountAsync();
        }

        public async Task<List<ErrorLog>> GetRecentAsync(Guid appId, int count)
        {
            return await _context.ErrorLogs
                .Where(e => e.ApplicationId == appId)
                .OrderByDescending(e => e.CreatedOn)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}