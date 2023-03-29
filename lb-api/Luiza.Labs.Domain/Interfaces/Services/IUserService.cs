using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.Models.Auth;
using Luiza.Labs.Domain.ViewModel;

namespace Luiza.Labs.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task AddUser(User user);
        Task<Token> AuthenticateAsync(LoginVM loginVM);
    }
}
