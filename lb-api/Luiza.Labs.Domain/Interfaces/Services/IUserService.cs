using Luiza.Labs.Domain.Models;

namespace Luiza.Labs.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task AuthenticateAsync(User user);
    }
}
