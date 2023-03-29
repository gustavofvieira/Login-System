using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.Models.Auth;

namespace Luiza.Labs.Domain.Interfaces.Services.Auth
{
    public interface ITokenService
    {
        Token GenerateToken(User user);
    }
}
