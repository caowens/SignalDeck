using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalDeck.Domain.Entities
{
    public class Metric
    {
        public int ApplicationId { get; set; }
        public Application Application { get; set; } = default!;
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
        public string? PropertiesAsJson { get; set; }
    }
}