using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.Event;
using SignalDeck.Application.Interfaces;
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
            var events = await _eventRepo.GetByApplicationIdAsync(appId);
            
            return events.Select(e => new EventDto
            {
                Id = e.Id,
                ApplicationId = e.ApplicationId,
                Name = e.Name,
                Timestamp = e.Timestamp,
                PropertiesAsJson = e.PropertiesAsJson
            });
        }

        public async Task<Event> CreateAsync(Event ev)
        {
            if (!await _appRepo.ExistsAsync(ev.ApplicationId))
            {
                throw new ArgumentException($"Application with ID {ev.ApplicationId} does not exist.");
            }
            
            var createdEvent = await _eventRepo.AddAsync(ev);

            return new Event
            {
                Id = createdEvent.Id,
                ApplicationId = createdEvent.ApplicationId,
                Name = createdEvent.Name,
                Timestamp = createdEvent.Timestamp,
                PropertiesAsJson = createdEvent.PropertiesAsJson
            };
        }
    }
}