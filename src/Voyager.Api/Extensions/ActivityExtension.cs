using System.Diagnostics;
using OpenTelemetry.Trace;

namespace Voyager.Api.Extensions;

public static class ActivityExtension
{
    public static void RecordErrorException(this Activity activity, Exception ex)
    {
        activity.SetStatus(ActivityStatusCode.Error);
        activity.RecordException(ex);
    }
}
