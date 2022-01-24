using System.Net;

namespace App.Application
{
    public class Response<T>
    {
        public Response() { }
        public Response(string errorMessage, HttpStatusCode statusCode)
        {
            this.IsSuccess = false;
            Errors = new List<string> { errorMessage };
            StatusCode = statusCode;
        }
        public Response(T data,HttpStatusCode statusCode = HttpStatusCode.OK, string message = "success")
        {
            IsSuccess = true;
            Result = data;
            Message = message;
            StatusCode = statusCode;
        }
        public bool IsSuccess { get; init; }
        public string Message { get; init; } = string.Empty;
        public List<string> Errors { get; init; } = new List<string>();
        public T? Result { get; init; } = default;
        public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;

    }
}
