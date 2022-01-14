using Microsoft.Extensions.Logging;

namespace App.Application.Extensions
{
    internal static class LoggerMessageDefinitions
    {
        public static Action<ILogger, T1, T2, Exception?> Log<T1, T2>(string message)
        {
            return LoggerMessage.Define<T1, T2>(LogLevel.Information, 0, message);
        }
        public static Action<ILogger, T1, Exception?> Log<T1>(string message)
        {
            return LoggerMessage.Define<T1>(LogLevel.Information, 0, message);
        }
        public static Action<ILogger,Exception?>Log(string message)
        {
            return LoggerMessage.Define(LogLevel.Information,0,message);
        }
    }
    internal static class LoggerExtension
    {
        public static void LogEx<T1, T2>(this ILogger logger, T1 t1, T2 t2, string message)
        {
            var logAction = LoggerMessageDefinitions.Log<T1,T2>(message);
            logAction(logger, t1, t2, null);
        }
        public static void LogEx<T1>(this ILogger logger, T1 t1, string message)
        {
            var logAction = LoggerMessageDefinitions.Log<T1>(message);
            logAction(logger, t1, null);
        }
        public static void LogEx(this ILogger logger, string message)
        {
            var logAction = LoggerMessageDefinitions.Log(message);
            logAction(logger, null);
        }
    }
}
