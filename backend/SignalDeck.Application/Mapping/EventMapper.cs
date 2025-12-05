using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.Event;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.Mapping
{
    public static class EventMapper
    {
        public static EventDto ToDto(this Event evModel)
        {
            return new EventDto
            {
                Id = evModel.Id,
                ApplicationId = evModel.ApplicationId,
                Name = evModel.Name,
                Timestamp = evModel.Timestamp,
                PropertiesAsJson = evModel.PropertiesAsJson
            };
        }

        public static Event ToEventFromCreateRequest(this CreateEventRequest createRequest)
        {
            return new Event
            {
                ApplicationId = createRequest.ApplicationId,
                Name = createRequest.Name,
                PropertiesAsJson = createRequest.PropertiesAsJson,
                Timestamp = DateTime.UtcNow
            };
        }
    }
}