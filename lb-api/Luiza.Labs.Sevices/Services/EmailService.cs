using Luiza.Labs.Domain.Enums;
using Luiza.Labs.Domain.Interfaces.Services;
using Luiza.Labs.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

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
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(mail.EmailAddressFrom);
            mensagem.To.Add(new MailAddress(mail.EmailAddressTo));
            mensagem.Subject = mail.Subject;
            mensagem.IsBodyHtml = true;
            mensagem.Body = mail.HtmlBody;

            SmtpClient cliente = new SmtpClient("smtp.office365.com", 587);
            cliente.Credentials = new NetworkCredential(mail.EmailAddressFrom, mail.Pass);
            cliente.EnableSsl = true;
            cliente.Send(mensagem);
        }

        public void SendConfirmation(User user)
        {
            _logger.LogInformation("[{0}] -  Started",nameof(SendConfirmation));

            var htmlBody = CreateBodyTemplate(user.Name, TemplateEmail.CreateSuccess);
            var mail = new Mail
            {
                Subject = $"Congratulations {user.Name}, your account has created with success!",
                EmailAddressTo = user.EmailAddress,
                NameSend = user.Name,
                Pass = "dE1@oito@1",
                EmailAddressFrom = "gu_conta_de_teste@outlook.com",
                HtmlBody = htmlBody,
            };
            
            SendMail(mail);
            _logger.LogInformation("[{0}] -  Finish",nameof(SendConfirmation));
        }

        public void SendRecovery(User user)
        {
            _logger.LogInformation("[{0}] -  Started", nameof(SendRecovery));

            //TODO: gerar token do usuário e mandar no link do email /updatePassword com o token no cabeçalho
            var htmlBody = CreateBodyTemplate(user.Name, TemplateEmail.RecoverPassword, $"http://localhost:4200/updatePassword/{user.UserId}");
            var mail = new Mail
            {
                Subject = "Recover Password!",
                HtmlBody = htmlBody,
                EmailAddressTo = user.EmailAddress,
                NameSend = user.Name,
                Pass = "dE1@oito@1",
                EmailAddressFrom = "gu_conta_de_teste@outlook.com"
            };

            SendMail(mail);
            _logger.LogInformation("[{0}] -  Finish", nameof(SendRecovery));
        }

        private string CreateBodyTemplate(string name, TemplateEmail template, string url = default!)
        {
            var htmlBody = template switch
            {
                TemplateEmail.CreateSuccess => System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "\\template\\template-create.html"),
                TemplateEmail.RecoverPassword => System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "\\template\\template-recovery.html")
            };

            htmlBody = htmlBody.Replace("{name}", name);
            if (template is TemplateEmail.RecoverPassword)
            {
                htmlBody = htmlBody.Replace("{url}", url);
            }
         
            return htmlBody;
        }
    }
}
