using Microsoft.AspNetCore.Mvc;
using SignalDeck.Api.Data;
using ApplicationEntity = SignalDeck.Api.Data.Entities.Application;

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
            var newApp = new ApplicationEntity
            {
                Name = name,
                ApiKey = Guid.NewGuid().ToString("N")
            };

            _context.Applications.Add(newApp);
            await _context.SaveChangesAsync();

            return Ok(newApp);
        }
    }
}