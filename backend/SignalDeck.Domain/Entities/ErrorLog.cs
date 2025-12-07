using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Domain.Common;

namespace SignalDeck.Domain.Entities
{
    public class ErrorLog : BaseEntity
    {
        public int ApplicationId { get; set; }
        public Application Application { get; set; } = default!;

        public string Message { get; set; } = string.Empty;
        public string? StackTrace { get; set; }
        public ErrorSeverity Severity { get; set; }
        public DateTime Timestamp { get; set; }
        public string? AdditionalDataAsJson { get; set; }
    }
}