using SignalDeck.Api.DTOs.Application;
using ApplicationEntity = SignalDeck.Api.Data.Entities.Application;

namespace SignalDeck.Application.Mapping
{
    public static class ApplicationMapper
    {
        public static ApplicationDto ToDto(this ApplicationEntity appModel)
        {
            return new ApplicationDto
            {
                Id = appModel.Id,
                Name = appModel.Name
            };
        }

        public static ApplicationEntity ToAppFromCreateRequest(this CreateApplicationRequest createRequest)
        {
            return new ApplicationEntity
            {
                Name = createRequest.Name
            };
        }
    }
}