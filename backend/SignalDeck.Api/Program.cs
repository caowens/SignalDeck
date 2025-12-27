using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using SignalDeck.Application.Interfaces;
using SignalDeck.Application.Services;
using SignalDeck.Application.Services.Applications;
using SignalDeck.Application.Services.ErrorLogs;
using SignalDeck.Application.Services.EventLogs;
using SignalDeck.Application.Services.Events;
using SignalDeck.Application.Services.Metrics;
using SignalDeck.Domain.Entities;
using SignalDeck.Infrastructure.Data;
using SignalDeck.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Select severity by name in Swagger documentation
    options.MapType<EventLogSeverity>(() =>
        new OpenApiSchema
        {
            Type = "string",
            Enum = Enum.GetNames(typeof(EventLogSeverity))
                    .Select(n => new OpenApiString(n))
                    .Cast<IOpenApiAny>()
                    .ToList()
        });
});

builder.Services.AddDbContext<SignalDeckDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SignalDeckDatabase"));
});

builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
builder.Services.AddScoped<IMetricRepository, MetricRepository>();
builder.Services.AddScoped<IEventLogRepository, EventLogRepository>();

builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IErrorLogService, ErrorLogService>();
builder.Services.AddScoped<IMetricService, MetricService>();
builder.Services.AddScoped<IEventLogService, EventLogService>();

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();