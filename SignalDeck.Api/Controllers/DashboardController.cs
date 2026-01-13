using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalDeck.Application.Services.Analytics;

namespace SignalDeck.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;
        public DashboardController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }
        
        [HttpGet("summary/{appId}")]
        public async Task<IActionResult> GetDashboardSummary(Guid appId)
        {
            var summary = await _analyticsService.GetSummaryAsync(appId);
            return Ok(summary);
        }
    }
}