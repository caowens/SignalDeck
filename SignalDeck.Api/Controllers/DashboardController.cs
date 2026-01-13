using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalDeck.Api.Data;

namespace SignalDeck.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly SignalDeckDbContext _context;
        public DashboardController(SignalDeckDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("summary/{appId}")]
        public async Task<IActionResult> GetDashboardSummary(Guid appId)
        {
            // var summary = await _analyticsService.GetSummaryAsync(appId);
            return Ok("Placeholder summary");
        }
    }
}