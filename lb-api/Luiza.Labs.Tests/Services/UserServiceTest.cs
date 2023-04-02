using AutoFixture;
using FluentValidation;
using Luiza.Labs.Domain.Interfaces.Repositories;
using Luiza.Labs.Domain.Interfaces.Services;
using Luiza.Labs.Domain.Interfaces.Services.Auth;
using Luiza.Labs.Domain.Models;
using Luiza.Labs.Sevices.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Luiza.Labs.Tests.Services
{
    public class UserServiceTest
    {
        
         private Mock<IValidator<User>> _validator = default!;
         private Mock<IUserRepository> _userRepository = default!;
         private Mock<ITokenService> _tokenService = default!;
         private Mock<IEmailService> _emailService = default!;
         private Mock<IRecoverPasswordService> _recoverPasswordService = default!;
         private Mock<ILogger<UserService>> _logger = default!;
         private Fixture _fixture = default!;

         [SetUp]
         public void SetUp()
         {
            _validator = new();
             _logger = new();
            _tokenService = new();
            _emailService = new();
            _recoverPasswordService = new();
            _userRepository = new();
             _fixture = new();
         }

        [Test]
        public async Task ShouldAddUserWithSuccess()
         {
             //Arrange
             var service = new UserService(_validator.Object,_logger.Object, _userRepository.Object, _tokenService.Object, _emailService.Object, _recoverPasswordService.Object);
             var user = _fixture.Build<User>().Create();

             //Act

             await service.Add(user);

             //Assert
             _logger.Verify(
                 x => x.Log(
                     LogLevel.Information,
                     It.IsAny<EventId>(),
                     It.Is<It.IsAnyType>((o, t) => string.Equals("[Add] - Started", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                     It.IsAny<Exception>(),
                     (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                 Times.Once);

             _logger.Verify(
                 x => x.Log(
                     LogLevel.Information,
                     It.IsAny<EventId>(),
                     It.Is<It.IsAnyType>((o, t) => string.Equals("[Add] - Finish", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                     It.IsAny<Exception>(),
                     (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                 Times.Once);

         }
    }
}
