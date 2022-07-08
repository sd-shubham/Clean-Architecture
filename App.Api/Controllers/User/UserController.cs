using App.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]
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
        [HttpGet("for-testing-only")]
        [Authorize(Roles ="Manager")]
        public async Task<ActionResult<string>> Test()
        {
            await Task.Delay(1000);
            return Ok(new JsonResult("Success"));
        }
    }
}