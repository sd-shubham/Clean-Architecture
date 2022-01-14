using System.Net;

namespace App.Application
{
    public class Response<T>
    {
        public Response() { }
        public Response(string message, HttpStatusCode statusCode)
        {
            this.IsSuccess = false;
            Errors = new List<string> { message };
            StatusCode = statusCode;
        }
        public Response(T data, string message = "success")
        {
            IsSuccess = true;
            Result = data;
            Message = message;
        }
        public bool IsSuccess { get; init; }
        public string Message { get; init; } = string.Empty;
        public List<string> Errors { get; init; } = new List<string>();
        public T? Result { get; init; } = default;
        public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;

    }
}
