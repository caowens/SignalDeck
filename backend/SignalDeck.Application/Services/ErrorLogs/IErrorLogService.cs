using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.ErrorLog;

namespace SignalDeck.Application.Services.ErrorLogs
{
    public interface IErrorLogService
    {
        Task<ErrorLogDto> CreateAsync(CreateErrorLogRequest request);
        Task<IEnumerable<ErrorLogDto>> GetByApplicationIdAsync(int appId);
    }
}