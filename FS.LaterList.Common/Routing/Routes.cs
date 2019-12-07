using System.Diagnostics.CodeAnalysis;

namespace FS.LaterList.Common.Routing
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class Routes
    {
        public const string ROUTE_PREFIX = "api";

        public class WeatherForecast
        {
            private const string ROOT = ROUTE_PREFIX + "/" + nameof(WeatherForecast) + "/";

            public const string Get = ROOT + nameof(Get);
        }
    }
}
