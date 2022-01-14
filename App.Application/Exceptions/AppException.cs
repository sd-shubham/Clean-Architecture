using System.Globalization;

namespace App.Application.Exceptions
{
    public class AppException: Exception
    {
        public AppException() : base() { }
        public AppException(string message) : base(message) { }
        public AppException(string message, params object[] arg)
               : base(string.Format(CultureInfo.CurrentCulture,message,arg))
        {

        }
    }
}
