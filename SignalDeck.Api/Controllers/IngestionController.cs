using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalDeck.Api.Data;
using SignalDeck.Api.Data.Entities;
using SignalDeck.Api.Mapping;
using SignalDeck.Sdk.Models;

namespace SignalDeck.Api.Controllers
{
    public class IngestionController : BaseApiController
    {
        private readonly SignalDeckDbContext _context;
        public IngestionController(SignalDeckDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Log([FromBody] SignalEvent signal)
        {
            var app = await GetAppByApiKey();

            if (app == null) return Unauthorized("Invalid API Key");

            var entity = signal.ToEntity(app.Id);

            _context.Signals.Add(entity);
            await _context.SaveChangesAsync();

            return Accepted();
        }

        [HttpPost("batch")]
        public async Task<IActionResult> LogBatch([FromBody] List<SignalEvent> signals)
        {
            var app = await GetAppByApiKey();

            if (app == null) return Unauthorized("Invalid API Key");

            var entities = signals.Select(s => s.ToEntity(app.Id)).ToList();

            _context.Signals.AddRange(entities);
            await _context.SaveChangesAsync();
            
            return Accepted();
        }

        private async Task<Application?> GetAppByApiKey()
        {
            if (!Request.Headers.TryGetValue("X-Signal-Key", out var apiKey))
                return null;
            
            return await _context.Applications
                .FirstOrDefaultAsync(a => a.ApiKey == apiKey.ToString());
        }
    }
}