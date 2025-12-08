using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalDeck.Application.DTOs.Event;
using SignalDeck.Application.Mapping;
using SignalDeck.Application.Services;

namespace SignalDeck.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly EventService _eventService;
        public EventsController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("application/{appId}")]
        public async Task<IActionResult> GetByApplicationId(int appId)
        {
            var events = await _eventService.GetByApplicationIdAsync(appId);
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventRequest request)
        {
            var evt = await _eventService.CreateAsync(request);
            return CreatedAtAction(nameof(GetByApplicationId), new { appId = evt.ApplicationId }, evt);
        }
    }
}