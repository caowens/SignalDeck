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

        public async Task<IEnumerable<ErrorLog>> GetByApplicationIdAsync(int appId)
        {
            return await _context.ErrorLogs
                .Where(e => e.ApplicationId == appId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}