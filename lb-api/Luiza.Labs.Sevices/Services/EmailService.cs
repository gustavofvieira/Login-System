using Luiza.Labs.Domain.Enums;
using Luiza.Labs.Domain.Exceptions;
using Luiza.Labs.Domain.Interfaces.Services;
using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.Models.Auth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Luiza.Labs.Sevices.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger _logger;
        private readonly AuthSmtp _authSmtp;
        private readonly FrontService _frontService;


        public EmailService(ILogger<EmailService> logger, IOptions<AuthSmtp> authSmtp, IOptions<FrontService> frontService)
        {
            _logger = logger;
            _authSmtp = authSmtp.Value;
            _frontService = frontService.Value;
        }

        public void SendMail(Mail mail) 
        {
            MailMessage mensagem = new MailMessage
            {
                From = new MailAddress(mail.EmailAddressFrom),
                Subject = mail.Subject,
                IsBodyHtml = true,
                Body = mail.HtmlBody,
                To = { new MailAddress(mail.EmailAddressTo) }
            };

            SmtpClient cliente = new SmtpClient(_authSmtp.SmtpHost, _authSmtp.SmtpPort)
            {
                Credentials = new NetworkCredential(_authSmtp.Email, _authSmtp.Password),
                EnableSsl = true
            };
            cliente.Send(mensagem);
        }

        public void SendConfirmation(User user)
        {
            _logger.LogInformation("[{Method}] -  Started", nameof(SendConfirmation));

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
            _logger.LogInformation("[{Method}] -  Finish", nameof(SendConfirmation));
        }

        public void SendRecovery(User user)
        {
            _logger.LogInformation("[{Method}] -  Started", nameof(SendRecovery));

            var htmlBody = CreateBodyTemplate(user.Name, TemplateEmail.RecoverPassword, $"{_frontService.Host}{_frontService.UpdatePassword}{user.UserId}");
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
            _logger.LogInformation("[{Method}] -  Finish", nameof(SendRecovery));
        }

        private string CreateBodyTemplate(string name, TemplateEmail template, string url = default!)
        {
            var htmlBody = template switch
            {
                TemplateEmail.CreateSuccess => File.ReadAllText(Directory.GetCurrentDirectory() + "\\template\\template-create.html"),
                TemplateEmail.RecoverPassword => File.ReadAllText(Directory.GetCurrentDirectory() + "\\template\\template-recovery.html"),
                _ => throw new TemplateEmailException("Type of Template E-mail not found")
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
