using Luiza.Labs.Domain.Interfaces.Services;
using MimeKit;
using MailKit.Net.Smtp;

namespace Luiza.Labs.Sevices.Services
{
    public class EmailService : IEmailService
    {
        //protected readonly ILogger _logger;
        public Task SendEmail(string emailAdress)
        {
            throw new NotImplementedException();
        }

        public void SendMail(string emailAdress)
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


        public void SendConfirmation(string emailAdress)
        {

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
