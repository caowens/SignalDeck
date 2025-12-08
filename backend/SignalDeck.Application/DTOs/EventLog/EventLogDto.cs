using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalDeck.Application.DTOs.EventLog
{
    public class EventLogDto
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Severity { get; set; } = "Info"; // Default
        public DateTime Timestamp { get; set; }
    }
}