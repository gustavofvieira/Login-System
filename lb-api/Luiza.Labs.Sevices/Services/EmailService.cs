using Luiza.Labs.Domain.Interfaces.Services;
using Luiza.Labs.Domain.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Luiza.Labs.Sevices.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger _logger;


        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public void SendMail(Mail mail)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(mail.NameSend, mail.EmailAddressFrom));
            email.To.Add(new MailboxAddress(mail.NameReceive, mail.EmailAddressTo));

            email.Subject = mail.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = mail.TextBody
            };
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.office365.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate(mail.EmailAddressFrom, mail.Pass);

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }


        public void SendConfirmation(User user)
        {
            _logger.LogInformation("[{0}] -  Started",nameof(SendConfirmation));
            var mail = new Mail
            {
                Subject = $"Congratulations {user.Name}, your account has created with success!",
                TextBody = "Welcome to LuizaLabs, Your account has created in the App!",
                EmailAddressTo = user.EmailAddress,
                NameSend = user.Name,
                Pass = "",
                EmailAddressFrom = "gu_conta_de_teste@outlook.com"
            };
            
            SendMail(mail);
            _logger.LogInformation("[{0}] -  Finish",nameof(SendConfirmation));
        }

        public void SendRecovery(User user)
        {

            _logger.LogInformation("[{0}] -  Started", nameof(SendRecovery));
            var mail = new Mail
            {
                Subject = "Recover Password!",
                TextBody = $"Hello {user.Name}, you requested the recover of the password, click in this link to recover password, case you're has not requested, ignore this e-mail",
                EmailAddressTo = user.EmailAddress,
                NameSend = user.Name,
                Pass = "",
                EmailAddressFrom = "gu_conta_de_teste@outlook.com"
            };

            SendMail(mail);
            _logger.LogInformation("[{0}] -  Finish", nameof(SendRecovery));
        }
    }
}
