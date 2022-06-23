using App.Application.Helper;
using System.Net;
using System.Runtime.CompilerServices;

namespace App.Application.Exceptions
{
    internal static class ErrorGaurd
    {
        public static void IsInvalid(this bool condition, string message)
        {
           HandleError(condition, $"{message}");
        }
        public static void IsExists(this bool condition, string message)
        {
            HandleError(condition, $"{message}");
        }
        public static void IsNull<T>(this T obj, string message)
        {
            HandleError((obj is null), $"{message}");
        }
        public static void NullOrEmpty(this string str, string message)
        {
            HandleError(string.IsNullOrEmpty(str),$"{message}",ExceptionType.ArgumentNull);
        }

        public static void HandleError(bool condition, 
                            [InterpolatedStringHandlerArgument("condition")] ref StringHandler message,
                            ExceptionType exceptionType = ExceptionType.NotFound,
                               [CallerArgumentExpression("condition")] string paramName = ""
                             )
        {
            var msg = message.ToString();
            if (condition)
            {
                throw AppException.GetExceptionByType(exceptionType, msg);
            }
        }
    }
}
