using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.EventLog;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.Mapping
{
    public static class EventLogMapper
    {
        public static EventLogDto ToDto(this EventLog eventLogModel)
        {
            return new EventLogDto
            {
                Id = eventLogModel.Id,
                ApplicationId = eventLogModel.ApplicationId,
                Message = eventLogModel.Message,
                Severity = eventLogModel.Severity.ToString(),
                Timestamp = eventLogModel.Timestamp
            };
        }

        public static EventLog ToEventLogFromCreateRequest(this CreateEventLogRequest createRequest)
        {
            return new EventLog
            {
                ApplicationId = createRequest.ApplicationId,
                Message = createRequest.Message,
                Severity = Enum.Parse<EventLogSeverity>(createRequest.Severity),
                Timestamp = DateTime.UtcNow
            };
        }
    }
}