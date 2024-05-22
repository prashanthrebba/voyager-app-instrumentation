using System.Diagnostics;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Voyager.Api.Utils;

namespace Voyager.Api.Extensions;

public static class VoyagerOpenTelemetryExtension
{
    public static IServiceCollection AddTelemetry(this IServiceCollection services)
    {
        var otlpEndpoint = "https://otlp.nr-data.net:4317";
        var appName = "Voyager.Api";
        var appNamespace = "Voyager";
        Telemetry.Init(appName);
        var resourceBuilder = ResourceBuilder.CreateDefault()
                                            .AddService(appName)
                                            .AddAttributes(new Dictionary<string, object>() { { "service.namespace", appNamespace } });
        services.AddOpenTelemetry()
            .WithTracing(tracerProviderBuilder => tracerProviderBuilder
                    .SetResourceBuilder(resourceBuilder)
                    .AddHttpClientInstrumentation(options =>
                    {
                        options.EnrichWithHttpRequestMessage = (activity, httpRequestMessage) => activity.SetTag("requestVersion", httpRequestMessage.Version);
                        options.EnrichWithHttpResponseMessage = (activity, httpResponseMessage) => activity.SetTag("responseVersion", httpResponseMessage.Version);
                        options.EnrichWithException = (activity, exception) => activity.SetTag("stackTrace", exception.StackTrace);
                    })
                    .AddSource(appName)
                    .SetErrorStatusOnException()
            .AddOtlpExporter(appName, options => options.Endpoint = new Uri(otlpEndpoint)))
            .WithMetrics(builder => builder
                .SetResourceBuilder(resourceBuilder)
                .AddProcessInstrumentation()
                .AddRuntimeInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter(options => options.Endpoint = new Uri(otlpEndpoint))
                .AddMeter(Telemetry.AppMeter)
                .SetResourceBuilder(resourceBuilder));

        services.AddLogging(builder => builder.AddOpenTelemetry(options =>
        {
            options.IncludeFormattedMessage = true;
            options.ParseStateValues = true;
            options.IncludeScopes = true;
            options.AddOtlpExporter(options => options.Endpoint = new Uri(otlpEndpoint));
            options.SetResourceBuilder(resourceBuilder);
        }));

        UnhandledExceptionActivityRecorder.ConfigureUnhandledExceptionActivityRecorder();
        return services;
    }
}

public static class UnhandledExceptionActivityRecorder
{
    public static void ConfigureUnhandledExceptionActivityRecorder()
        => AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

    private static void UnhandledExceptionHandler(object source, UnhandledExceptionEventArgs args)
    {
        var ex = (Exception)args.ExceptionObject;

        var activity = Activity.Current;

        while (activity != null)
        {
            activity.RecordException(ex);
            activity.Dispose();
            activity = activity.Parent;
        }
    }
}
