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
    public class EventRepository : IEventRepository
    {
        private readonly SignalDeckDbContext _context;
        public EventRepository(SignalDeckDbContext context)
        {
            _context = context;
        }
        public async Task<Event> AddAsync(Event ev)
        {
            await _context.Events.AddAsync(ev);
            await _context.SaveChangesAsync();
            return ev;
        }

        public async Task<IEnumerable<Event>> GetByApplicationIdAsync(Guid appId)
        {
            return await _context.Events
                .Where(e => e.ApplicationId == appId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}