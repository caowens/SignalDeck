using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Domain.Common;
using ApplicationEntity = SignalDeck.Domain.Entities.Application;

namespace SignalDeck.Domain.Entities
{
    public class ErrorLog : BaseEntity
    {
        public int ApplicationId { get; set; }
        public ApplicationEntity Application { get; set; } = default!;

        public string Message { get; set; } = string.Empty;
        public string? StackTrace { get; set; }
        public ErrorSeverity Severity { get; set; } = ErrorSeverity.Low;
        public DateTime Timestamp { get; set; }
        public string? AdditionalDataAsJson { get; set; }
    }
}