using Luiza.Labs.Domain.Models;

namespace Luiza.Labs.Domain.Interfaces.Services.Auth
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
