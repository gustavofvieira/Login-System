using FluentValidation;
using Luiza.Labs.Domain.Exceptions;
using Luiza.Labs.Domain.Interfaces.Repositories;
using Luiza.Labs.Domain.Interfaces.Services;
using Luiza.Labs.Domain.Interfaces.Services.Auth;
using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.ViewModel;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace Luiza.Labs.Sevices.Services
{
    public class UserService : IUserService
    {
        private readonly IValidator<User> _validator;
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository; 
        private readonly ITokenService _tokenService;

        public UserService(
            IValidator<User> validator, 
            ILogger<UserService> logger,
            IUserRepository userRepository,
            ITokenService tokenService, 
            IEmailService emailService)
        {
            _validator = validator;
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public async Task AddUser(User user)
        {
            
            try
            {
                _logger.LogInformation("[{0}] - Started", nameof(AddUser));
                _validator.ValidateAndThrow(user);
                user.Password = EncryptPassword(user.Password);
                await _userRepository.AddUser(user);

                _logger.LogInformation("Send Email to: {0}", user.EmailAdress);
                _emailService.SendConfirmation(user.EmailAdress);
                _logger.LogInformation("[{0}] Send Email with success", nameof(AddUser));
                _logger.LogInformation("[{0}] - Finish", nameof(AddUser));
            }
            catch(Exception ex)
            {
                _logger.LogError("[{0}] is failed! with message: {1}", nameof(AddUser), ex.Message);
                throw new DomainException(ex.Message);
            }
        }

        public async Task<string> AuthenticateAsync(LoginVM loginVM)
        {
            loginVM.Password = EncryptPassword(loginVM.Password);
            var userRepository = await _userRepository.AuthenticateAsync(loginVM);

            if (userRepository == null)
                throw new DomainException("User not found.");

           var token = _tokenService.GenerateToken(userRepository);
           return token;
        }

        public string EncryptPassword(string senha)
        {
            //byte[] bytes = Encoding.UTF8.GetBytes(senha);
            //using (SHA256Managed sha256 = new SHA256Managed())
            //{
            //    byte[] hash = sha256.ComputeHash(bytes);
            //    return Convert.ToBase64String(hash);
            //}
            byte[] bytes = Encoding.UTF8.GetBytes(senha);
            SHA256Managed sha256 = new SHA256Managed();
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string senhaDigitada, string senhaCadastrada)
        {
            var senhaHash = EncryptPassword(senhaDigitada);

            return senhaHash == senhaCadastrada;
        }
    }
}
