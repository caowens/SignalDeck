using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.DTOs.ErrorLog
{
    public class ErrorLogDto
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? StackTrace { get; set; }
        public ErrorSeverity Severity { get; set; }
        public DateTime Timestamp { get; set; }
        public string? AdditionalDataAsJson { get; set; }
    }
}