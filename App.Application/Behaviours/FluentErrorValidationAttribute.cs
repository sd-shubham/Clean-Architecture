using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace App.Application.Behaviours
{
    public class FluentErrorValidationAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0)
                             .SelectMany(v => v.Errors)
                             .Select(e => e.ErrorMessage).ToList();
                var problem = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest

                };
                problem.Extensions.Add("Errors",errors);
                context.Result = new JsonResult(problem);
            }
        }
    }
}
