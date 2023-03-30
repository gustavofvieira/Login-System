using Luiza.Labs.Domain.Interfaces.Services;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Luiza.Labs.Domain.Models;

namespace Luiza.Labs.Sevices.Services
{
    public class EmailService : IEmailService
    {
        protected readonly ILogger _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }
        public Task SendEmail(string emailAdress)
        {
            throw new NotImplementedException();
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


        public void SendConfirmation(string emailAdress)
        {
            _logger.LogInformation("[{0}] -  Started",nameof(SendConfirmation));
            var mail = new Mail
            {
                Subject = $"Congratulations {emailAdress}, your account has created with success!",
                TextBody = "Welcome to LuizaLabs, Your account has created in the App!",
                EmailAddressTo = emailAdress,
                NameSend = emailAdress,
                Pass = "",
                EmailAddressFrom = "gu_conta_de_teste@outlook.com"
            };
            
            SendMail(mail);
            _logger.LogInformation("[{0}] -  Finish",nameof(SendConfirmation));
        }

        public void SendRecovery(string emailAdress)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Luiza Labs", emailAdress));
            email.To.Add(new MailboxAddress("Receiver Name", emailAdress));

            email.Subject = "Testing out email sending";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = "Hello all the way from the land of C#"
            };
            using (var smtp = new SmtpClient())
            {
                //smtp.Connect("smtp.server.address", 587, false);
                smtp.Connect("smtp.office365.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                //smtp.Authenticate("smtp_username", "smtp_password");
                smtp.Authenticate("gu_conta_de_teste@outlook.com", "dE1@oito@1");

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}
