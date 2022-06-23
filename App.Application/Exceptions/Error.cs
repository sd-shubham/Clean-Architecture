using App.Application.Helper;
using System.Net;
using System.Runtime.CompilerServices;

namespace App.Application.Exceptions
{
    internal static class ErrorGaurd
    {
        public static void IsInvalid(this bool obj,
           [InterpolatedStringHandlerArgument("obj")] ref GenericStringHandler<bool> message,
           [CallerArgumentExpression("obj")] string paramName = "")
        {
            if (obj)
                throw new NotFoundException(message.ToString(), paramName);
        }
        public static void IsExists(this bool obj,
           [InterpolatedStringHandlerArgument("obj")] ref GenericStringHandler<bool> message,
           [CallerArgumentExpression("obj")] string paramName = "")
        {
            if (obj)
                throw new NotFoundException(message.ToString(), paramName);
        }
        public static void IsNull<T>(this T  obj,
              [InterpolatedStringHandlerArgument("obj")] ref GenericStringHandler<T> message,
              [CallerArgumentExpression("obj")] string paramName = "")
        {
            if (obj is null)
                throw new NotFoundException(message.ToString(), paramName);
        }
    }
}
