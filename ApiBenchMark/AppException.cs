using System.Globalization;

namespace App.Application.Exceptions
{
    public abstract class AppException: Exception
    {
        public AppException() : base() { }
        public AppException(string message) : base(message) { }
        public AppException(string message, params object[] arg)
               : base(string.Format(CultureInfo.CurrentCulture,message,arg))
        {

        }
        public static AppException GetExceptionByType(ExceptionType type, string message)
        {
           return type switch
            {
                ExceptionType.NotFound => new NotFoundException(message),
                ExceptionType.ArgumentNull => new ArgumentsNullException(message),
                _ => new NotFoundException(message),
            };
        }
    }
    public class NotFoundException: AppException
    {
        public NotFoundException()
        {

        }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, params object[] arg)
               : base(string.Format(CultureInfo.CurrentCulture, message, arg)){}
    }
    public class BadRequestException : AppException
    {
        public BadRequestException(){}
        public BadRequestException(string message) : base(message) { }
        public BadRequestException(string message, params object[] arg)
               : base(string.Format(CultureInfo.CurrentCulture, message, arg)){}
    }
    public class ArgumentsNullException : AppException
    {
        public ArgumentsNullException() { }
        public ArgumentsNullException(string message) : base(message) { }
        public ArgumentsNullException(string message, params object[] arg)
               : base(string.Format(CultureInfo.CurrentCulture, message, arg)) { }
    }
    public enum ExceptionType
    {
        NotFound,
        ArgumentNull,
        ArgumentOutOfRange,
        Custome
    }
}
