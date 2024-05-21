// using System.Diagnostics.Metrics;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure the OpenTelemetry SDK for traces and metrics
builder.Services.AddOpenTelemetry()
    .UseOtlpExporter()
    .ConfigureResource(resourceBuilder =>
    {
        resourceBuilder
            .AddService(serviceName: "getting-started-dotnet",
                        serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown");
    })
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .AddSource(nameof(app_instrumentation))
            .AddAspNetCoreInstrumentation();
    })
    .WithMetrics(meterProviderBuilder =>
    {
        meterProviderBuilder
            .AddMeter(nameof(app_instrumentation))
            .AddAspNetCoreInstrumentation();
        // .AddRuntimeInstrumentation()
        // .AddView(instrument =>
        // {
        //     return instrument.GetType().GetGenericTypeDefinition() == typeof(Histogram<>)
        //         ? new Base2ExponentialBucketHistogramConfiguration()
        //         : null;
        // });
    });

// Configure the OpenTelemetry SDK for logs
builder.Logging.ClearProviders();
builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeFormattedMessage = true;
    options.ParseStateValues = true;
    options.IncludeScopes = true;
});


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
