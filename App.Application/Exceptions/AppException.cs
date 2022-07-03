using System.Globalization;
using System.Net;

namespace App.Application.Exceptions
{
    public abstract class AppException: Exception
    {
        public  HttpStatusCode StatusCode { get; } 
        public AppException(HttpStatusCode statusCode) : base() {
            StatusCode = statusCode;
        }
        public AppException(string message, HttpStatusCode statusCode) : base(message) {
            StatusCode = statusCode;
        }
        public AppException(string message, HttpStatusCode statusCode, params object[] arg)
               : base(string.Format(CultureInfo.CurrentCulture,message,arg))
        {
            StatusCode = statusCode;
        }
        public static AppException GetExceptionByType(ExceptionType type, string message)
        {
           return type switch
            {
                ExceptionType.NotFound => new NotFoundException(message, HttpStatusCode.NotFound),
                ExceptionType.ArgumentNull => new ArgumentsNullException(message,HttpStatusCode.InternalServerError),
                _ => new NotFoundException(message, HttpStatusCode.NotFound),
            };
        }
    }
    public class NotFoundException: AppException
    {
        public NotFoundException(HttpStatusCode statusCode): base(statusCode)
        {

        }
        public NotFoundException(string message, HttpStatusCode statusCode) : base(message, statusCode) { }
        public NotFoundException(string message, HttpStatusCode statusCode, params object[] arg)
               : base(string.Format(CultureInfo.CurrentCulture, message, arg),statusCode){}
    }
    public class BadRequestException : AppException
    {
        public BadRequestException(HttpStatusCode statusCode):base(statusCode) {}
        public BadRequestException(string message, HttpStatusCode statusCode) : base(message, statusCode) { }
        public BadRequestException(string message, HttpStatusCode statusCode, params object[] arg)
               : base(string.Format(CultureInfo.CurrentCulture, message, arg), statusCode){}
    }
    public class ArgumentsNullException : AppException
    {
        public ArgumentsNullException(HttpStatusCode statusCode): base(statusCode) { }
        public ArgumentsNullException(string message, HttpStatusCode statusCode) : base(message,statusCode) { }
        public ArgumentsNullException(string message, HttpStatusCode statusCode, params object[] arg)
               : base(string.Format(CultureInfo.CurrentCulture, message, arg), statusCode) { }
    }
    public enum ExceptionType
    {
        NotFound,
        ArgumentNull,
        ArgumentOutOfRange,
        Custome
    }
}
