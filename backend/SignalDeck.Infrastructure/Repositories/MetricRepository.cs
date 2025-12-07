using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SignalDeck.Application.Interfaces;
using SignalDeck.Domain.Entities;
using SignalDeck.Infrastructure.Data;

namespace SignalDeck.Infrastructure.Repositories
{
    public class MetricRepository : IMetricRepository
    {
        private readonly SignalDeckDbContext _context;
        public MetricRepository(SignalDeckDbContext context)
        {
            _context = context;
        }

        public async Task<Metric> AddAsync(Metric metric)
        {
            await _context.Metrics.AddAsync(metric);
            await _context.SaveChangesAsync();
            return metric;
        }

        public async Task<IEnumerable<Metric>> GetByApplicationIdAsync(int appId)
        {
            return await _context.Metrics
                .Where(m => m.ApplicationId == appId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Metric>> QueryMetricsAsync(int appId, string metricName)
        {
            return await _context.Metrics
                .Where(m => m.ApplicationId == appId && m.Name == metricName)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}