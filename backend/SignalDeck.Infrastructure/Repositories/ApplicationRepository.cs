using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SignalDeck.Application.Interfaces;
using SignalDeck.Infrastructure.Data;
using ApplicationEntity = SignalDeck.Domain.Entities.Application;

namespace SignalDeck.Infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly SignalDeckDbContext _context;
        public ApplicationRepository(SignalDeckDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationEntity> AddAsync(ApplicationEntity app)
        {
            await _context.Applications.AddAsync(app);
            await  _context.SaveChangesAsync();
            return app;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Applications.AnyAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<ApplicationEntity>> GetAllAsync()
        {
            return await _context.Applications.AsNoTracking().ToListAsync();
        }

        public async Task<ApplicationEntity?> GetByIdAsync(int id)
        {
            return await _context.Applications.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}