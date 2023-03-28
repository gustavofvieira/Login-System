using Luiza.Labs.Domain.Interfaces.Repositories;
using Luiza.Labs.Domain.Interfaces.Services;
using Luiza.Labs.Domain.Interfaces.Services.Auth;
using Luiza.Labs.Domain.Models;
using System.Net;

namespace Luiza.Labs.Sevices.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository; 
        private readonly ITokenService _tokenService; 

        public UserService(IUserRepository userRepository,ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> AuthenticateAsync(User user)
        {
            var userRepository = await _userRepository.AuthenticateAsync(user);

            if (userRepository == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

           var token = _tokenService.GenerateToken(user);
           return token;
        }
    }
}
