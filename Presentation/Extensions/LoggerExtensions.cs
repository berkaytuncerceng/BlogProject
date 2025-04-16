using NLog.Web;

namespace Presentation.Extensions
{
    public static class LoggerExtensions
    {
        public static void ConfigureLogger(this ILoggingBuilder logging)
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Information);
        }

        public static void UseNLogConfiguration(this IHostBuilder host) =>
            host.UseNLog();
    }
}