using App.Application.Exceptions;
using FluentValidation;
using FluentValidation.Results;
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

             (string errorMessage, HttpStatusCode statusCode, IEnumerable<string> errors) = (exception.Error) switch
            {
                ValidationException err => ("", HttpStatusCode.BadRequest, err.Errors.Select(x => x.ErrorMessage)),
                AppException error => (error?.Message ?? error?.InnerException?.Message 
                                      ?? "error occored while processing record", error.StatusCode ,null),
                _ => ("un-handeled error",HttpStatusCode.BadRequest,null)

            };
            var problemDetail = new ProblemDetails
            {
                Status = (int)statusCode,
                Detail = errorMessage ?? "",
            };
            problemDetail.Extensions.Add("Errors", errors);
            return new JsonResult(problemDetail);
            //return Problem(errorMessage,
            //    statusCode: (int)statusCode);
        }
    }
}
