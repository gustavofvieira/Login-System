using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.ViewModel;

namespace Luiza.Labs.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> AuthenticateAsync(LoginVM loginVM);
        Task<User> GetUserByEmail(string email);
        Task UpdatePassword(User user);
    }
}
