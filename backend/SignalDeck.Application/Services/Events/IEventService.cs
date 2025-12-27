using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.Event;

namespace SignalDeck.Application.Services.Events
{
    public interface IEventService
    {
        Task<EventDto> CreateAsync(CreateEventRequest request);
        Task<IEnumerable<EventDto>> GetByApplicationIdAsync(int appId);
    }
}