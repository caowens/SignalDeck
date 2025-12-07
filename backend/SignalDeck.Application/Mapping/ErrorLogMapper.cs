using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.ErrorLog;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.Mapping
{
    public static class ErrorLogMapper
    {
        public static ErrorLogDto ToDto(this ErrorLog errorLogModel)
        {
            return new ErrorLogDto
            {
                Id = errorLogModel.Id,
                ApplicationId = errorLogModel.ApplicationId,
                Message = errorLogModel.Message,
                Severity = errorLogModel.Severity.ToString(),
                Timestamp = errorLogModel.Timestamp
            };
        }

        public static ErrorLog ToErrorLogFromCreateRequest(this CreateErrorLogRequest createRequest)
        {
            return new ErrorLog
            {
                ApplicationId = createRequest.ApplicationId,
                Message = createRequest.Message,
                StackTrace = createRequest.StackTrace,
                Severity = Enum.Parse<ErrorSeverity>(createRequest.Severity),
                Timestamp = DateTime.UtcNow,
                AdditionalDataAsJson = createRequest.AdditionalDataAsJson,
            };
        }
    }
}