using App.Application.Helper;
using System.Runtime.CompilerServices;

namespace App.Application.Exceptions
{
    internal static class ErrorGaurd
    {

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
        public static void IsExists(this bool condition,
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
