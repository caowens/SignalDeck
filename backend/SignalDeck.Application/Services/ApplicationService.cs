using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.Application;
using SignalDeck.Application.Interfaces;
using SignalDeck.Application.Mapping;
using ApplicationEntity = SignalDeck.Domain.Entities.Application;

namespace SignalDeck.Application.Services
{
    public class ApplicationService
    {
        private readonly IApplicationRepository _appRepo;

        public ApplicationService(IApplicationRepository appRepo)
        {
            _appRepo = appRepo;
        }

        public async Task<IEnumerable<ApplicationDto>> GetAllAsync()
        {
            var apps = await _appRepo.GetAllAsync();
            return apps.Select(a => new ApplicationDto
            {
                Id = a.Id,
                Name = a.Name
            });
        }

        public async Task<ApplicationEntity> CreateAsync(ApplicationEntity app)
        {
            var createdApp = await _appRepo.AddAsync(app);

            return new ApplicationEntity
            {
                Id = createdApp.Id,
                Name = createdApp.Name
            };
        }
    }
}