namespace Luiza.Labs.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmail(string emailAdress);
        void SendRecovery(string emailAdress);
        void SendConfirmation(string emailAdress);
    }
}
