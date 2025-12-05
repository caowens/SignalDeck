using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalDeck.Application.DTOs.Event
{
    public class CreateEventRequest
    {
        public int ApplicationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? PropertiesAsJson { get; set; }
    }
}