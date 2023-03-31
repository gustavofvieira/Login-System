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
        // GET: api/<LoginController>
        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User?.Identity?.Name);


        [HttpGet]
        [Route("common")]
        [Authorize(Roles = Roles.Common+","+ Roles.Adm)]
        public string Common() => Roles.Common;

        [HttpGet]
        [Route("adm")]
        [Authorize(Roles = Roles.Adm)]
        public string Adm() => Roles.Adm;


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
            return Ok();
        }


        [HttpPost]
        [Route("recoverPassword")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> RecoverPassword([FromBody] string email)
        {
            await _userService.RecoverEmail(email);
            return Ok();
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = Roles.Adm)]
        public async Task<IActionResult> GetAll()
        {
            //_logger.LogInformation("[{Method}] - Started ", nameof(GetAll));
            var users = await _userService.GetAll();
            return Ok(users);
        }
    }
}
