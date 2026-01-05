using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.Metric;

namespace SignalDeck.Application.Services.Metrics
{
    public interface IMetricService
    {
        Task<MetricDto> CreateAsync(CreateMetricRequest request);
        Task<IEnumerable<MetricDto>> GetByApplicationIdAsync(Guid appId);
        Task<IEnumerable<MetricDto>> GetMetricsByNameAsync(Guid appId, string metricName);
    }
}