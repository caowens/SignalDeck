using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalDeck.Api.Data;
using SignalDeck.Api.Data.Entities;
using SignalDeck.Api.DTOs;
using SignalDeck.Api.Mapping;

namespace SignalDeck.Api.Controllers
{
    public class ApplicationsController : BaseApiController
    {
        private readonly SignalDeckDbContext _context;
        public ApplicationsController(SignalDeckDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Applications
                .Select(app => app.ToSidebarDto())
                .ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<AppSidebarDto>> Create([FromBody] string name)
        {
            string randomPart = Guid.NewGuid().ToString("N");
            string formattedKey = $"sd_live_{randomPart}";
            
            var newApp = new Application
            {
                Name = name,
                ApiKey = formattedKey
            };

            _context.Applications.Add(newApp);
            await _context.SaveChangesAsync();

            var appDto = newApp.ToSidebarDto();

            return Ok(appDto);
        }
    }
}