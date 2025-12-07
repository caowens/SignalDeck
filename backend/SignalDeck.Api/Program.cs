using Microsoft.EntityFrameworkCore;
using SignalDeck.Application.Interfaces;
using SignalDeck.Application.Services;
using SignalDeck.Infrastructure.Data;
using SignalDeck.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SignalDeckDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SignalDeckDatabase"));
});

builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
builder.Services.AddScoped<IMetricRepository, MetricRepository>();

builder.Services.AddScoped<ApplicationService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<ErrorLogService>();
builder.Services.AddScoped<MetricService>();

builder.Services.AddControllers();

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