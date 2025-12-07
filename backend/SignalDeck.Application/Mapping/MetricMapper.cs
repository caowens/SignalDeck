using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Application.DTOs.Metric;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Application.Mapping
{
    public static class MetricMapper
    {
        public static MetricDto ToDto(this Metric metricModel)
        {
            return new MetricDto
            {
                Id = metricModel.Id,
                ApplicationId = metricModel.ApplicationId,
                Name = metricModel.Name,
                Value = metricModel.Value,
                Timestamp = metricModel.Timestamp,
                PropertiesAsJson = metricModel.PropertiesAsJson
            };
        }

        public static Metric ToMetricFromCreateRequest(this CreateMetricRequest createRequest)
        {
            return new Metric
            {
                ApplicationId = createRequest.ApplicationId,
                Name = createRequest.Name,
                Value = createRequest.Value,
                Timestamp = createRequest.Timestamp,
                PropertiesAsJson = createRequest.PropertiesAsJson
            };
        }
    }
}