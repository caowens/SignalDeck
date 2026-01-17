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

        public static AppSidebarDto ToDto(this Application app)
        {
            return new AppSidebarDto(
                app.Id,
                app.Name,
                app.ApiKey
            );
        }
    }
}