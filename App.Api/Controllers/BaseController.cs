using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    public abstract class BaseController: ControllerBase
    {
        private readonly ISender _mediator;
        protected ISender Mediator => _mediator ?? HttpContext.RequestServices
                                                                .GetService<ISender>();
    }
}
