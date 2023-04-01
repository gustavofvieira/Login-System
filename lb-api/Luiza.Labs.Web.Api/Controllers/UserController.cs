using Luiza.Labs.Domain.Constants;
using Luiza.Labs.Domain.Interfaces.Services;
using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.Models.Auth;
using Luiza.Labs.Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luiza.Labs.Web.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public async Task<ActionResult<string>> Authenticated() => Ok(String.Format("Hello {0}, Welcome!", User?.Identity?.Name));

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<Token>> Authenticate([FromBody] LoginVM model)
        {
            var token = await _userService.AuthenticateAsync(model);
            return Ok(token);
        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Create([FromBody] User model)
        {
            await _userService.Add(model);
            return Ok("Created with success!");
        }


        [HttpPost]
        [Route("recoverPassword")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> RecoverPassword([FromBody] string email)
        {
            await _userService.RecoverEmail(email);
            return Ok("Link to recover password sended to your e-mail, you have 5 minutes to validate");
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = Roles.Adm)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpPost]
        [Route("updatePassword/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdatePassword([FromRoute] Guid id, [FromBody] string password)
        {
            await _userService.UpdatePassword(id, password);
            return Ok("Password update with success!");
        }
    }
}
