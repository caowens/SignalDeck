using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalDeck.Application.DTOs.ErrorLog;
using SignalDeck.Application.Services;

namespace SignalDeck.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErrorLogController : ControllerBase
    {
        private readonly ErrorLogService _errorLogService;
        public ErrorLogController(ErrorLogService errorLogService)
        {
            _errorLogService = errorLogService;
        }

        [HttpGet("application/{appId}")]
        public async Task<IActionResult> GetByApplicationId(int appId)
        {
            var errorLogs = await _errorLogService.GetByApplicationIdAsync(appId);
            return Ok(errorLogs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateErrorLogRequest request)
        {
            var createdErrorLog = await _errorLogService.CreateAsync(request);
            return CreatedAtAction(nameof(GetByApplicationId), new { appId = createdErrorLog.ApplicationId }, createdErrorLog);
        }
    }
}