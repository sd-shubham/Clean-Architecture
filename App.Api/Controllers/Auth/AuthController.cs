using App.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    //todo - remove this controller since we are using Azure Ad for auth.
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create(RegisterUserCommand userDto)
        {
            return Ok( await Mediator.Send(userDto));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand userLoginDto)
        {
            return Ok(await Mediator.Send(userLoginDto));
        }
    }
}
