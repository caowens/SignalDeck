using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.Event;
using SignalDeck.Application.Interfaces;
using SignalDeck.Application.Mapping;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.Services
{
    public class EventService
    {
        private readonly IEventRepository _eventRepo;
        private readonly IApplicationRepository _appRepo;

        public EventService(IEventRepository eventRepo, IApplicationRepository appRepo)
        {
            _eventRepo = eventRepo;
            _appRepo = appRepo;
        }

        public async Task<IEnumerable<EventDto>> GetByApplicationIdAsync(int appId)
        {
            bool appExists = await _appRepo.ExistsAsync(appId);
            if (!appExists)
            {
                throw new ArgumentException($"Application with ID {appId} does not exist.");
            }
            
            var events = await _eventRepo.GetByApplicationIdAsync(appId);
            
            return events.Select(e => e.ToDto()).ToList();
        }

        public async Task<EventDto> CreateAsync(CreateEventRequest request)
        {
            bool appExists = await _appRepo.ExistsAsync(request.ApplicationId);
            if (!appExists)
            {
                throw new ArgumentException($"Application with ID {request.ApplicationId} does not exist.");
            }
            
            var evt = request.ToEventFromCreateRequest();
            await _eventRepo.AddAsync(evt);
            return evt.ToDto();
        }
    }
}