using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.Application;
using ApplicationEntity = SignalDeck.Domain.Entities.Application;

namespace SignalDeck.Application.Services.Applications
{
    public interface IApplicationService
    {
        Task<ApplicationEntity> CreateAsync(ApplicationEntity app);
        Task<IEnumerable<ApplicationDto>> GetAllAsync();
    }
}