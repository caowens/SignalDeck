using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.Interfaces
{
    public interface IMetricRepository
    {
        Task<IEnumerable<Metric>> GetByApplicationIdAsync(int appId);
        Task<Metric> AddAsync(Metric metric);
        Task<IEnumerable<Metric>> QueryMetricsAsync(int appId, string metricName);
    }
}