using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.Analytics;

namespace SignalDeck.Application.Services.Analytics
{
    public interface IAnalyticsService
    {
        Task<DashboardSummaryDto> GetSummaryAsync(Guid applicationId);
    }
}