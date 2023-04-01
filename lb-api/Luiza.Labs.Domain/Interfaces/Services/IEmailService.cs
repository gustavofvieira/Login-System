using Luiza.Labs.Domain.Models;

namespace Luiza.Labs.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        void SendRecovery(User user, Guid recoverId);
        void SendConfirmation(User user);
    }
}
