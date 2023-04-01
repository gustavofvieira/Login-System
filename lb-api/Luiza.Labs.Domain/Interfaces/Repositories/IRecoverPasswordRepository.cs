using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.ViewModel;

namespace Luiza.Labs.Domain.Interfaces.Repositories
{
    public interface IRecoverPasswordRepository
    {
        Task Add(RecoverPassword recoverPassword);
        Task<RecoverPassword> GetById(Guid id);
    }
}
