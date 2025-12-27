using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.Metric;
using SignalDeck.Application.Interfaces;
using SignalDeck.Application.Mapping;
using SignalDeck.Application.Services.Metrics;

namespace SignalDeck.Application.Services
{
    public class MetricService : IMetricService
    {
        private readonly IMetricRepository _metricRepo;
        private readonly IApplicationRepository _appRepo;
        public MetricService(IMetricRepository metricRepo, IApplicationRepository appRepo)
        {
            _metricRepo = metricRepo;
            _appRepo = appRepo;
        }

        public async Task<IEnumerable<MetricDto>> GetByApplicationIdAsync(int appId)
        {
            bool appExists = await _appRepo.ExistsAsync(appId);
            if (!appExists)
            {
                throw new ArgumentException($"Application with ID {appId} does not exist.");
            }

            var metrics = await _metricRepo.GetByApplicationIdAsync(appId);
            return metrics.Select(m => m.ToDto()).ToList();
        }

        public async Task<MetricDto> CreateAsync(CreateMetricRequest request)
        {
            bool appExists = await _appRepo.ExistsAsync(request.ApplicationId);
            if (!appExists)
            {
                throw new ArgumentException($"Application with ID {request.ApplicationId} does not exist.");
            }

            var metric = request.ToMetricFromCreateRequest();
            await _metricRepo.AddAsync(metric);
            return metric.ToDto();
        }

        public async Task<IEnumerable<MetricDto>> GetMetricsByNameAsync(int appId, string metricName)
        {
            bool appExists = await _appRepo.ExistsAsync(appId);
            if (!appExists)
            {
                throw new ArgumentException($"Application with ID {appId} does not exist.");
            }

            bool metricExists = await _metricRepo.ExistsAsync(metricName);
            if (!metricExists)
            {
                throw new ArgumentException($"Metric with name {metricName} does not exist.");
            }

            var metrics = await _metricRepo.QueryMetricsAsync(appId, metricName);
            return metrics.Select(m => m.ToDto()).ToList();
        }
    }
}