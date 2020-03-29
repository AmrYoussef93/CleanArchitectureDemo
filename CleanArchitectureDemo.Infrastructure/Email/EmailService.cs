using CleanArchitectureDemo.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureDemo.Infrastructure.Email.Models;
using Serilog;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Linq;
using MailKit.Net.Smtp;

namespace CleanArchitectureDemo.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly EmailConfiguration _emailConfiguration;
        public EmailService(IWebHostEnvironment webHostEnvironment, ILogger logger, IOptions<EmailConfiguration> emailConfiguration)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _emailConfiguration = emailConfiguration.Value;
        }
        public async Task SendRegistrationEmailAsyn(string name, string email, string confirmationCode)
        {
            var templatePath = GetTemplatePath("RegistrationTemplate.html");
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = File.OpenText(templatePath))
            {
                builder.HtmlBody = string.Format(SourceReader.ReadToEnd(), name, confirmationCode);
            }
            var subject = "Welcome to northwind system";
            var emailMessage = new EmailContent()
            {
                Body = builder.ToMessageBody(),
                Subject = subject,
                To = email
            };

            await Send(emailMessage);
        }
        private async Task Send(EmailContent email)
        {
            try
            {
                var message = new MimeMessage();
                message.To.Add(new MailboxAddress(email.To));
                if (email.Cc.Count > 0)
                    message.Cc.AddRange(email.Cc.Select(x => new MailboxAddress(x)));
                message.From.Add(new MailboxAddress(_emailConfiguration.From));
                message.Subject = email.Subject;
                message.Body = email.Body;
                using (var emailClient = new SmtpClient() { })
                {
                    emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    emailClient.Connect(_emailConfiguration.Server, _emailConfiguration.Port);
                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    emailClient.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);
                    await emailClient.SendAsync(message);
                    emailClient.Disconnect(true);
                }
            }
            catch (Exception exp)
            {
                _logger.Error(exp, "Error while sending email");
            }
        }

        private string GetTemplatePath(string templateName)
        {
            var pathToFile = _webHostEnvironment.WebRootPath
                    + Path.DirectorySeparatorChar.ToString()
                    + "EmailTemplates"
                    + Path.DirectorySeparatorChar.ToString()
                    + templateName;
            return pathToFile;
        }
    }
}
