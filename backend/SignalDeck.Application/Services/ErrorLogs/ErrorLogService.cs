using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.ErrorLog;
using SignalDeck.Application.Interfaces;
using SignalDeck.Application.Mapping;
using SignalDeck.Application.Services.ErrorLogs;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.Services
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IErrorLogRepository _errorLogRepo;
        private readonly IApplicationRepository _appRepo;

        public ErrorLogService(IErrorLogRepository errorLogRepo, IApplicationRepository appRepo)
        {
            _errorLogRepo = errorLogRepo;
            _appRepo = appRepo;
        }

        public async Task<IEnumerable<ErrorLogDto>> GetByApplicationIdAsync(int appId)
        {
            bool appExists = await _appRepo.ExistsAsync(appId);
            if (!appExists)
            {
                throw new KeyNotFoundException($"Application with ID {appId} does not exist.");
            }

            var errorLogs = await _errorLogRepo.GetByApplicationIdAsync(appId);
            return errorLogs.Select(el => el.ToDto()).ToList();
        }

        public async Task<ErrorLogDto> CreateAsync(CreateErrorLogRequest request)
        {
            bool appExists = await _appRepo.ExistsAsync(request.ApplicationId);
            if (!appExists)
            {
                throw new KeyNotFoundException($"Application with ID {request.ApplicationId} does not exist.");
            }

            if (!IsValidSeverity(request.Severity))
            {
                throw new ArgumentException($"Severity '{request.Severity}' is invalid. Valid values: {string.Join(", ", Enum.GetNames(typeof(ErrorSeverity)))}");
            }

            var errorLog = request.ToErrorLogFromCreateRequest();
            await _errorLogRepo.AddAsync(errorLog);
            return errorLog.ToDto();
        }

        private bool IsValidSeverity(string severity)
        {
            return Enum.TryParse<ErrorSeverity>(severity, true, out _);
        }

    }
}