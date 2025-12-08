using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Domain.Common;

namespace SignalDeck.Domain.Entities
{
    public class EventLog : BaseEntity
    {
        public int ApplicationId { get; set; }
        public Application Application { get; set; } = default!;
        public string Message { get; set; } = string.Empty;
        public LogSeverity Severity { get; set; } = LogSeverity.Info;
        public DateTime Timestamp { get; set; }
        public string? PropertiesAsJson { get; set; }
    }

    public enum LogSeverity
    {
        Trace,
        Debug,
        Info,
        Warning,
        Error,
        Critical
    }
}