using Microsoft.AspNetCore.Mvc;
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

            return Ok(newApp.ToDto());
        }
    }
}