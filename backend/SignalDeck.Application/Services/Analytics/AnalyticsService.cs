using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.Analytics;
using SignalDeck.Application.DTOs.ErrorLog;
using SignalDeck.Application.DTOs.Event;
using SignalDeck.Application.Mapping;
using SignalDeck.Application.Persistence;

namespace SignalDeck.Application.Services.Analytics
{
    public class AnalyticsService : IAnalyticsService
    {
        private const int DashboardLimit = 5;
        private readonly IApplicationRepository _appRepo;
        private readonly IErrorLogRepository _errorRepo;
        private readonly IEventRepository _eventRepo;
        public AnalyticsService(IApplicationRepository appRepo, IErrorLogRepository errorRepo, IEventRepository eventRepo)
        {
            _appRepo = appRepo;
            _errorRepo = errorRepo;
            _eventRepo = eventRepo;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync(Guid applicationId)
        {
            var app = await _appRepo.GetByIdAsync(applicationId);
            if (app == null)
            {
                throw new Exception("Application not found");
            }

            var yesterday = DateTime.UtcNow.AddDays(-1);
            var errorCount = await _errorRepo.GetCountSinceAsync(applicationId, yesterday);
            var eventCount = await _eventRepo.GetCountSinceAsync(applicationId, yesterday);
            var recentErrors = await _errorRepo.GetRecentAsync(applicationId, DashboardLimit);
            var recentEvents = await _eventRepo.GetRecentAsync(applicationId, DashboardLimit);

            return new DashboardSummaryDto
            {
                ApplicationId = applicationId,
                ApplicationName = app.Name,
                TotalEvents24H = eventCount,
                TotalErrors24H = errorCount,
                ErrorRatePercentage = eventCount == 0 ? 0 : (double)errorCount / eventCount * 100,
                RecentErrors = recentErrors.Select(er => er.ToDto()).ToList(),
                RecentEvents = recentEvents.Select(ev => ev.ToDto()).ToList()
            };


        }
    }
}