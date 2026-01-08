using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.Persistence
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetByApplicationIdAsync(Guid appId);
        Task<Event> AddAsync(Event ev);
    }
}