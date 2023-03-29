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
        //private readonly HashAlgorithm _algorithm;
        private readonly IValidator<User> _validator;
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository; 
        private readonly ITokenService _tokenService;

        public UserService(
            //HashAlgorithm algorithm,
            IValidator<User> validator, ILogger<UserService> logger,IUserRepository userRepository,ITokenService tokenService, IEmailService emailService)
        {
            //_algorithm = algorithm;
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
                user.Password = CriptografarSenha(user.Password);
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



        public string CriptografarSenha(string senha)
        {
            var _algorithm = HashAlgorithm.Create();
            var encodedValue = Encoding.UTF8.GetBytes(senha);
            var encryptedPassword = _algorithm.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }

        public bool VerificarSenha(string senhaDigitada, string senhaCadastrada)
        {
            var _algorithm = HashAlgorithm.Create(); 
            if (string.IsNullOrEmpty(senhaCadastrada))
                throw new NullReferenceException("Cadastre uma senha.");

            var encryptedPassword = _algorithm.ComputeHash(Encoding.UTF8.GetBytes(senhaDigitada));

            var sb = new StringBuilder();
            foreach (var caractere in encryptedPassword)
            {
                sb.Append(caractere.ToString("X2"));
            }

            return sb.ToString() == senhaCadastrada;
        }

        public async Task<string> AuthenticateAsync(LoginVM loginVM)
        {
            var userRepository = await _userRepository.AuthenticateAsync(loginVM);

            if (userRepository == null)
                throw new DomainException("User not found.");

           var token = _tokenService.GenerateToken(userRepository);
           return token;
        }
    }
}
