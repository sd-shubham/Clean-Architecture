using App.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace App.Api.Controllers
{
    //todo - remove this controller since we are using Azure Ad for auth.
    [ApiController]
    [Route("api/auth")]
    [FeatureGate("IsEnable")]
    public class AuthController : BaseController
    {
        private readonly IFeatureManager _featureManger;

        public AuthController(IFeatureManager featureManger)
        {
            _featureManger = featureManger;
        }

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
        [HttpPost("featureflag-testing")]
        public async Task<IActionResult> Test()
        {
            var isEnable = await _featureManger.IsEnabledAsync("IsEnable");

            var somedummyObj = new
            {
                Name = "shubham",
                valueBasedOnFeatureFlag = isEnable ? "some value" : null
            };
            return Ok(somedummyObj);
        }
    }
}
