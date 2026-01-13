using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.ErrorLog;
using SignalDeck.Application.DTOs.Event;

namespace SignalDeck.Api.DTOs.Dashboard
{
    public class DashboardSummaryDto
    {
        public Guid ApplicationId { get; set; }
        public string ApplicationName { get; set; } = string.Empty;

        public int TotalEvents24H { get; set; }
        public int TotalErrors24H { get; set; }
        public double ErrorRatePercentage { get; set; } // Percentage of errors over total events

        public List<ErrorLogDto> RecentErrors { get; set; } = new();
        public List<EventDto> RecentEvents { get; set; } = new();
    }
}