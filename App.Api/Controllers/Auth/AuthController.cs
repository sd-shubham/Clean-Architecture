using App.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
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
