using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Sdk.Models;

namespace SignalDeck.Api.Data.Entities
{
    public class SignalEntity
    {
        // Database-specific fields
        [Key]
        public int InternalId { get; set; }
        [Required]
        public int ApplicationId { get; set; }
        public Application Application { get; set; } = default!;
        public DateTime ServerRecievedAt { get; set; } = DateTime.UtcNow;

        // Data mirrored from the event
        public Guid ExternalId { get; set; }
        public string Name { get; set; } = string.Empty;
        public SignalSeverity Severity { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool IsError => Severity >= SignalSeverity.Error;
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public DateTime EventTimestamp { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new();
    }
}