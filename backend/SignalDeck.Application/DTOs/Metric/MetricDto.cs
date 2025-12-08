using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalDeck.Application.DTOs.Metric
{
    public class MetricDto
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
        public Dictionary<string, object>? PropertiesAsJson { get; set; }
    }
}