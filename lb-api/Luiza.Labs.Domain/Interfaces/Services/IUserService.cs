using Luiza.Labs.Domain.Models;

namespace Luiza.Labs.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(User user);
    }
}
