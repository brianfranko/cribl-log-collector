using System.Net.Http;
using LogCollector.Clients;
using LogCollector.Configuration;
using LogCollector.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(s =>
{
    var logCollectorConfiguration = new LogCollectorConfiguration();
    var config = s.GetService<IConfiguration>();
    config.GetSection(LogCollectorConfiguration.LogCollector).Bind(logCollectorConfiguration);
    return logCollectorConfiguration;
});
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddScoped<IEventReaderService, EventReaderService>();
builder.Services.AddScoped<IEventFilterService, EventFilterService>();
builder.Services.AddScoped<ILogCollectorClient, LogCollectorClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
