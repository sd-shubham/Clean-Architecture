using App.Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("/error")]
    public class ErrorController : ControllerBase
    {
        [HttpGet,HttpPost] // for swagger
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = exception.Error as AppException;
            return Problem(error?.Message ?? error?.InnerException?.Message ?? "error occored while processing record",
                statusCode: (int)error.StatusCode);
        }
    }
}
