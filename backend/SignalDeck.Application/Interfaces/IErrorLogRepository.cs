using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.Interfaces
{
    public interface IErrorLogRepository
    {
        Task<IEnumerable<ErrorLog>> GetByApplicationIdAsync(int appId);
        Task<ErrorLog> AddAsync(ErrorLog errorLog);
    }
}