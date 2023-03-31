using FluentValidation;
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
        private readonly IEmailService _emailService;
        public UserController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }
        // GET: api/<LoginController>
        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        // GET api/<LoginController>/5
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anonimo";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee, manager")]
        public string Employee() => "Funcionario";


        // POST api/<LoginController>
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
        //[Authorize(Roles = Roles.Manager)]
        public async Task<ActionResult<string>> Create([FromBody] User model)
        {
            await _userService.AddUser(model);
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
    }
}
