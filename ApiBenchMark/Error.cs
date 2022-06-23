using App.Application.Helper;
using System.Runtime.CompilerServices;

namespace App.Application.Exceptions
{
    public static class ErrorGaurd
    {
        public static void IsInvalid(this bool condition, string message)
        {
           HandleError(condition, $"{message}");
        }
        public static void IsExists(this bool condition, string message)
        {
            HandleError(condition, $"{message}");
        }
        public  static bool  IsNull<T>(this T obj)
        {
            return obj is null;
        }
        public static void NullOrEmpty(this string str, [InterpolatedStringHandlerArgument("str")] ref StringHandler message)
        {
            HandleError(string.IsNullOrEmpty(str),ref message,ExceptionType.ArgumentNull);
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
        public static void NullOrEmptySimple(this string str, string message)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new Exception(message);
            }
        }
        public static void IsTrue(this bool condition,
                           [InterpolatedStringHandlerArgument("condition")] ref StringHandler message,
                           ExceptionType exceptionType = ExceptionType.NotFound,
                              [CallerArgumentExpression("condition")] string paramName = ""
                            )
        {
            if (condition)
            {
                throw AppException.GetExceptionByType(exceptionType, message.ToString());
            }
        }
    }
}
