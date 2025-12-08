using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.DTOs.ErrorLog
{
    public class CreateErrorLogRequest
    {
        public int ApplicationId { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? StackTrace { get; set; }
        public string Severity { get; set; } = "Low"; // Default
        public Dictionary<string, object>? AdditionalDataAsJson { get; set; }
    }
}