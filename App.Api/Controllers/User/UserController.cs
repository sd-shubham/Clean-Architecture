using App.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : BaseController
    {
        
        [HttpGet]
        public async Task<IActionResult>Get()
            =>Ok(await Mediator.Send(new GetAllUserQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            return Ok(await Mediator.Send(new GetUserById(id)));
        }
    }
}