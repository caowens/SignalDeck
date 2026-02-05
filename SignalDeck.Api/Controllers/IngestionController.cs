using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalDeck.Api.Data;
using SignalDeck.Api.Mapping;
using SignalDeck.Sdk.Models;

namespace SignalDeck.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class IngestionController : ControllerBase
    {
        private readonly SignalDeckDbContext _context;
        public IngestionController(SignalDeckDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Log(SignalEvent signal)
        {
            if (!Request.Headers.TryGetValue("X-Signal-Key", out var apiKey))
                return Unauthorized("Missing API Key");

            var app = await _context.Applications
                .FirstOrDefaultAsync(a => a.ApiKey == apiKey.ToString());

            if (app == null) return Unauthorized("Invalid API Key");

            var entity = signal.ToEntity(app.Id);

            _context.Signals.Add(entity);
            await _context.SaveChangesAsync();

            return Accepted();
        }
    }
}