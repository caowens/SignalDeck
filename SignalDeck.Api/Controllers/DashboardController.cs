using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalDeck.Api.Data;
using SignalDeck.Api.DTOs;
using SignalDeck.Api.Mapping;

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
        
        [HttpGet("{appId:int}/stats")]
        public async Task<IActionResult> GetStats(int appId)
        {

            var signals = await _context.Signals
                .Where(s => s.ApplicationId == appId)
                .AsNoTracking()
                .ToListAsync();

            if (!signals.Any()) return Ok(new AppStatsDto(0, 0, 0.0, "None"));

            var stats = signals.ToStatsDto();

            return Ok(stats);
        }

        [HttpGet("{appId:int}/chart")]
        public async Task<IActionResult> GetChartData(int appId)
        {
            return Ok(await _context.Signals
                .Where(s => s.ApplicationId == appId && s.EventTimestamp > DateTime.UtcNow.AddDays(-7))
                .GroupBy(s => s.EventTimestamp.Date)
                .OrderBy(g => g.Key)
                .Select(g => new ChartPointDto(g.Key.ToShortDateString(), g.Count()))
                .ToListAsync());
        }
    }
}