using Luiza.Labs.Domain.Models;

namespace Luiza.Labs.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(User user);
    }
}
