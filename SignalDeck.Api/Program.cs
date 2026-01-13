using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using SignalDeck.Api.Data;
using SignalDeck.Sdk.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Select severity by name in Swagger documentation
    options.MapType<SignalSeverity>(() =>
        new OpenApiSchema
        {
            Type = "string",
            Enum = Enum.GetNames(typeof(SignalSeverity))
                    .Select(n => new OpenApiString(n))
                    .Cast<IOpenApiAny>()
                    .ToList()
        });
});

builder.Services.AddDbContext<SignalDeckDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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