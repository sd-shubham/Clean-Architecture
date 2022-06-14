using App.Application.Helper;
using System.Net;
using System.Runtime.CompilerServices;

namespace App.Application.Exceptions
{
    // do not  use this one
    internal static class Error<T> where T : notnull
    {
        public static Response<T> NotFound(string errorMessage = "not found")
        {
            return new Response<T>(errorMessage, HttpStatusCode.NotFound);
        }
        public static Response<T> BadRequest(string errorMessage = "somthing went wrong")
        {
            return new Response<T>(errorMessage, HttpStatusCode.BadRequest);
        }
        // allocation free.
    }

    // use this instead.
    internal static class ErrorGaurd
    {
        //public static void IsNotTrue(this bool condition,
        //      [InterpolatedStringHandlerArgument("condition")] ref StringHandler message,
        //      [CallerArgumentExpression("condition")] string paramName = "")
        //{
        //    if (!condition)
        //        throw new BadRequestException(message.ToString(), paramName);
        //}
        public static void IsNotTrue(this bool obj,
             [InterpolatedStringHandlerArgument("obj")] ref GenericStringHandler<bool> message,
             [CallerArgumentExpression("obj")] string paramName = "")
        {
            if (!obj)
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
