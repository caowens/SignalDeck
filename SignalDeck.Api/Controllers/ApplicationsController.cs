using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalDeck.Api.Data;
using SignalDeck.Api.Data.Entities;
using SignalDeck.Api.Mapping;

namespace SignalDeck.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApplicationsController : ControllerBase
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
        public async Task<IActionResult> Create([FromBody] string name)
        {
            var newApp = new Application
            {
                Name = name,
                ApiKey = Guid.NewGuid().ToString("N")
            };

            _context.Applications.Add(newApp);
            await _context.SaveChangesAsync();

            return Ok(newApp.ToSidebarDto());
        }
    }
}