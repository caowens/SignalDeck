using SignalDeck.Api.Data.Entities;
using SignalDeck.Api.DTOs;
using SignalDeck.Sdk.Models;

namespace SignalDeck.Api.Mapping
{
    public static class MappingExtensions
    {
        public static SignalEntity ToEntity(this SignalEvent sdkEvent, int appId)
        {
            return new SignalEntity
            {
                ExternalId = sdkEvent.Id,
                ApplicationId = appId,
                Name = sdkEvent.Name,
                Severity = sdkEvent.Severity,
                Category = sdkEvent.Category,
                Message = sdkEvent.Message,
                StackTrace = sdkEvent.StackTrace,
                EventTimestamp = sdkEvent.Timestamp,
                Properties = sdkEvent.Properties
            };
        }

        public static AppSidebarDto ToSidebarDto(this Application app)
        {
            return new AppSidebarDto(
                app.Id,
                app.Name,
                app.ApiKey
            );
        }

        public static AppStatsDto ToStatsDto(this IEnumerable<SignalEntity> signals)
        {
            var signalList = signals.ToList();
            if (!signalList.Any())
            {
                return new AppStatsDto(0, 0, 0.0, string.Empty);
            }

            var total = signalList.Count;
            var errors = signalList.Count(s => s.IsError);
            var topSignal = signalList
                .GroupBy(s => s.Name)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault() ?? "None";

            return new AppStatsDto(
                total,
                errors,
                total > 0 ? (double)errors / total : 0.0,
                topSignal
            );
        }
    }
}