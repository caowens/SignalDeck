using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalDeck.Application.DTOs.EventLog
{
    public class CreateEventLogRequest
    {
        public int ApplicationId { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Severity { get; set; } = "Info"; // Default
        public string? PropertiesAsJson { get; set; }
    }
}