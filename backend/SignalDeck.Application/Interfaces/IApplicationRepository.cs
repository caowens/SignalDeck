using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationEntity = SignalDeck.Domain.Entities.Application;

namespace SignalDeck.Application.Interfaces
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<ApplicationEntity>> GetAllAsync();
        Task<ApplicationEntity?> GetByIdAsync(int id);
        Task<ApplicationEntity> AddAsync(ApplicationEntity app);
        Task<bool> ExistsAsync(int id);
    }
}