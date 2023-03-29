using FluentValidation;
using Luiza.Labs.Domain.Constants;
using Luiza.Labs.Domain.Enums;
using Luiza.Labs.Domain.Interfaces.Services;
using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.Models.Auth;
using Luiza.Labs.Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Luiza.Labs.Web.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IValidator<User> _validator;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public UserController(IValidator<User> validator,IUserService userService, IEmailService emailService)
        {
            _validator = validator;
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
        [Authorize(Roles = Roles.Manager)]
        public async Task<ActionResult<string>> Create([FromBody] User model)
        {
            await _userService.AddUser(model);
            return Ok();
        }


        [HttpPost]
        [Route("recoverPassword")]
        [AllowAnonymous]
        public ActionResult RecoverPassword([FromBody] string email)
        {
            _emailService.SendRecovery(email);
            return Ok();
        }
    }
}
