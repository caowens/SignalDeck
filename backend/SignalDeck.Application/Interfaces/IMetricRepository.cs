using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.Interfaces
{
    public interface IMetricRepository
    {
        Task<IEnumerable<Metric>> GetByApplicationIdAsync(Guid appId);
        Task<Metric> AddAsync(Metric metric);
        Task<IEnumerable<Metric>> QueryMetricsAsync(Guid appId, string metricName);
        Task<bool> ExistsAsync(string metricName);
    }
}