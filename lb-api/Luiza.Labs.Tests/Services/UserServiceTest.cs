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
         private Mock<ILogger<UserService>> _logger = default!;
         private Fixture _fixture = default!;

         [SetUp]
         public void SetUp()
         {
            _validator = new();
             _logger = new();
            _tokenService = new();
            _userRepository = new();
             _fixture = new();
         }

        [Test]
        public async Task ShouldAddWithSuccess()
         {
             //Arrange
             var service = new UserService(_validator.Object,_logger.Object, _userRepository.Object, _tokenService.Object, _emailService.Object);
             var user = _fixture.Build<User>().Create();

             //Act

             await service.AddUser(user);

             //Assert
             _logger.Verify(
                 x => x.Log(
                     LogLevel.Information,
                     It.IsAny<EventId>(),
                     It.Is<It.IsAnyType>((o, t) => string.Equals("[AddUser] - Started", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                     It.IsAny<Exception>(),
                     (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                 Times.Once);

             _logger.Verify(
                 x => x.Log(
                     LogLevel.Information,
                     It.IsAny<EventId>(),
                     It.Is<It.IsAnyType>((o, t) => string.Equals("[AddUser] - Finish", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                     It.IsAny<Exception>(),
                     (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                 Times.Once);

         }
    }
}
