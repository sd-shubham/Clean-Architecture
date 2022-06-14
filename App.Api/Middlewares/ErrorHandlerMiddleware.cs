using App.Application;
using App.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace App.Api.Middlewares
{
    internal class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                // set content- type
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>(error?.Message ?? "somthing went wrong", HttpStatusCode.BadRequest);
                response.StatusCode = error switch
                {
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    BadRequestException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError,
                };
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
