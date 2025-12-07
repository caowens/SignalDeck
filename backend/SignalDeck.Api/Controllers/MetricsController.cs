using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalDeck.Application.DTOs.Metric;
using SignalDeck.Application.Services;

namespace SignalDeck.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricsController : ControllerBase
    {
        private readonly MetricService _metricService;
        public MetricsController(MetricService metricService)
        {
            _metricService = metricService;
        }

        [HttpGet("application/{appId}")]
        public async Task<IActionResult> GetByApplicationId(int appId)
        {
            var metrics = await _metricService.GetByApplicationIdAsync(appId);
            return Ok(metrics);
        }

        [HttpGet("{appId}/name/{metricName}")]
        public async Task<IActionResult> GetByName(int appId, string metricName)
        {
            var metrics = await _metricService.GetMetricsByNameAsync(appId, metricName);
            if (metrics == null || !metrics.Any())
            {
                return NotFound("Metrics at that name or application are not found.");
            }
            return Ok(metrics);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMetricRequest request)
        {
            var metric = await _metricService.CreateAsync(request);
            return CreatedAtAction(nameof(GetByApplicationId), new { appId = metric.ApplicationId }, metric);
        }
    }
}