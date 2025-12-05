using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetByApplicationIdAsync(int appId);
        Task<Event> AddAsync(Event ev);
    }
}