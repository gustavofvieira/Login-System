using Luiza.Labs.Domain.Models;

namespace Luiza.Labs.Domain.Interfaces.Services
{
    public interface IRecoverPasswordService
    {
        Task<RecoverPassword> ValidExpirateDate(Guid id);
        Task<Guid> CreateRecoverPassword(User user);
    }
}
