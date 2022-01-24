using Microsoft.Extensions.Logging;

namespace App.Application.Extensions
{
    internal static class LoggerMessageDefinitions
    {
        public static void Log(this ILogger logger, string message)
        {
            var loggerAction = LoggerMessage.Define(LogLevel.Information, 0, message);
            loggerAction(logger, null);
        }
        public static void Log<T1>(this ILogger logger, T1 t1, string message)
        {
            var loggerAction = LoggerMessage.Define<T1>(LogLevel.Information, 0, message);
            loggerAction(logger, t1, null);
        }
        public static void Log<T1, T2>(this ILogger logger, T1 t1, T2 t2, string message)
        {
            var loggerAction = LoggerMessage.Define<T1, T2>(LogLevel.Information, 0, message);
            loggerAction(logger, t1, t2, null);
        }
    }
}
