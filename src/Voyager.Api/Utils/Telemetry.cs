using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Voyager.Api.Utils;

[ExcludeFromCodeCoverage]
public static class Telemetry
{
    public static ActivitySource? AppActivitySource { get; private set; }
    public static string? _appMeter;
    public static string AppMeter
    {
        get
        {
            if (string.IsNullOrEmpty(_appMeter))
            {
                throw new InvalidOperationException("AppMeter is not initialized");
            }
            return _appMeter;
        }
        private set => _appMeter = value;
    }

    public static void Init(string serviceName)
    {
        AppMeter = serviceName;
        if (AppActivitySource == null)
        {
            AppActivitySource = new(serviceName);
        }
        else
        {
            throw new InvalidOperationException("ActivitySource is already initialized");
        }
    }
}
