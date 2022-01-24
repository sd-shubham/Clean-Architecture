using System.Net;

namespace App.Application.Exceptions
{
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
    }
}
