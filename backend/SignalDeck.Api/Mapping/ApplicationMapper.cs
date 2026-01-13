using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.Application;
using ApplicationEntity = SignalDeck.Domain.Entities.Application;

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