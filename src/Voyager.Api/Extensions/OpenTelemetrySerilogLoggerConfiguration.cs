using Serilog;

namespace Voyager.Api.Extensions;

public static class OpenTelemetrySerilogLoggerConfiguration
{
    public static readonly Action<HostBuilderContext, LoggerConfiguration> LoggerConfigurator =
            (hostingContext, loggerConfiguration) =>
            {
                var otlpEndpoint = "https://otlp.nr-data.net:4317";
                var appName = "Voyager.Api";
                var appNamespace = "Voyager";

                loggerConfiguration
                .WriteTo.OpenTelemetry(cfg =>
                {
                    cfg.Endpoint = otlpEndpoint;
                    cfg.ResourceAttributes = new Dictionary<string, object>
                    {
                            {"service.name", appName },
                            {"service.namespace", appNamespace}
                    };
                });
            };
}
