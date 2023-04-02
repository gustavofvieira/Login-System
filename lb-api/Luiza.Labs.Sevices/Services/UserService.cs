using FluentValidation;
using Luiza.Labs.Domain.Exceptions;
using Luiza.Labs.Domain.Interfaces.Repositories;
using Luiza.Labs.Domain.Interfaces.Services;
using Luiza.Labs.Domain.Interfaces.Services.Auth;
using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.Models.Auth;
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
        private readonly IRecoverPasswordService _recoverPasswordService;

        public UserService(
            IValidator<User> validator, 
            ILogger<UserService> logger,
            IUserRepository userRepository,
            ITokenService tokenService, 
            IEmailService emailService,
            IRecoverPasswordService recoverPasswordService)
        {
            _validator = validator;
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _emailService = emailService;
            _recoverPasswordService = recoverPasswordService;
        }

        public async Task Add(User user)
        {
            
            try
            {

                _logger.LogInformation("[{0}] - Started", nameof(Add));

                var userBd = await _userRepository.GetUserByEmail(user.EmailAddress);
                if (userBd is not null)
                    throw new DomainException("E-mail Has Existent");

                _validator.ValidateAndThrow(user);
                user.Password = EncryptPassword(user.Password);
                await _userRepository.Add(user);

                _logger.LogInformation("Send Email to: {0}", user.EmailAddress);
                _emailService.SendConfirmation(user);
                _logger.LogInformation("[{Method}] Send Email with success", nameof(Add));
                _logger.LogInformation("[{Method}] - Finish", nameof(Add));
            }
            catch(Exception ex)
            {
                _logger.LogError("[{Method}] is failed! with message: {Message}", nameof(Add), ex.Message);
                throw new DomainException(ex.Message);
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task RecoverEmail(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(email);
                if (user is null)
                    throw new DomainException("User Not Found");
                
               var recoverId = await _recoverPasswordService.CreateRecoverPassword(user);

                _emailService.SendRecovery(user, recoverId);
            }
            catch(Exception ex)
            {
                _logger.LogError("[{Method}] is failed! with message: {Message}", nameof(RecoverEmail), ex.Message);
                throw new DomainException(ex.Message);
            }
        }

        public async Task<Token> AuthenticateAsync(LoginVM loginVM)
        {
            loginVM.Password = EncryptPassword(loginVM.Password);
            var userRepository = await _userRepository.AuthenticateAsync(loginVM);

            if (userRepository == null)
                throw new DomainException("E-mail or password wrong!");

           var token = _tokenService.GenerateToken(userRepository);
           return token;
        }

        public string EncryptPassword(string senha)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(senha);
            SHA256Managed sha256 = new SHA256Managed();
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public async Task<List<User>> GetAll()
        {
            _logger.LogInformation("[{Mehtod}] - Started", nameof(GetAll));
            var users = await _userRepository.GetAll();
            _logger.LogInformation("[{Mehtod}] - Finish", nameof(GetAll));
            return users;
        }

        public async Task<User> GetById(Guid id)
        {
            _logger.LogInformation("[{Mehtod}] - Started, with ID: {id}", nameof(GetById), id);
            var user = await _userRepository.GetById(id);
            _logger.LogInformation("[{Mehtod}] - Finish, with ID: {id}", nameof(GetById), id);
            return user;
        }

        public async Task Update(User user)
        {
            _logger.LogInformation("[{Mehtod}] - Started, with ID: {id}", nameof(Update), user.UserId);
            await _userRepository.Update(user);
            _logger.LogInformation("[{Mehtod}] - Finish, with ID: {id}", nameof(Update), user.UserId);
        }

        public async Task UpdatePassword(Guid id, string password)
        {

            _logger.LogInformation("[{Mehtod}] - Started, with ID: {id}", nameof(UpdatePassword),id);

            var recover = await _recoverPasswordService.ValidExpirateDate(id);

            var user = await _userRepository.GetById(recover.UserId);
            if (user is null)
                throw new DomainException("User Not Found");

            user.Password = EncryptPassword(password);

            await _userRepository.UpdatePassword(user);
            _logger.LogInformation("[{Mehtod}] - Finish, with ID: {id}", nameof(UpdatePassword), id);
        }

        public async Task Remove(Guid id)
        {
            _logger.LogInformation("[{Mehtod}] - Started, with ID: {id}", nameof(Remove), id);
            await _userRepository.Remove(id);
            _logger.LogInformation("[{Mehtod}] - Finish, with ID: {id}", nameof(Remove), id);
        }
    }
}
