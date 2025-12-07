using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalDeck.Application.DTOs.Metric
{
    public class CreateMetricRequest
    {
        public int ApplicationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
        public string? PropertiesAsJson { get; set; }
    }
}