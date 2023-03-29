using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.ViewModel;

namespace Luiza.Labs.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> AuthenticateAsync(LoginVM loginVM);
    }
}
