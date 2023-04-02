using Luiza.Labs.Domain.Interfaces.Repositories;
using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.Models.Auth;
using Luiza.Labs.Domain.ViewModel;

namespace Luiza.Labs.Domain.Interfaces.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<Token> AuthenticateAsync(LoginVM loginVM);
        Task RecoverEmail(string email);
        Task UpdatePassword(Guid id, string password);
    }
}
