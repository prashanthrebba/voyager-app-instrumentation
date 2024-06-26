// using System.Diagnostics.Metrics;
using System.Diagnostics;
using Serilog;
using Voyager.Api.Extensions;
using Voyager.Api.Services;
using Voyager.Api.ServicesImpl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTelemetry();
builder.Services.AddScoped<IJokeService, JokeService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();


// builder.Host.UseSerilog(OpenTelemetrySerilogLoggerConfiguration.LoggerConfigurator);

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
