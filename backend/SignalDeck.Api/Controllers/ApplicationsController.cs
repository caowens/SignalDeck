using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalDeck.Application.DTOs.Application;
using SignalDeck.Application.Services;
using SignalDeck.Application.Mapping;

namespace SignalDeck.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly ApplicationService _appService;
        public ApplicationsController(ApplicationService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var apps =  await _appService.GetAllAsync();
            return Ok(apps);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateApplicationRequest request)
        {
            var createdApp = request.ToAppFromCreateRequest();
            await _appService.CreateAsync(createdApp);
            return CreatedAtAction(nameof(GetAll), new { id = createdApp.Id }, createdApp.ToDto());
        }
    }
}