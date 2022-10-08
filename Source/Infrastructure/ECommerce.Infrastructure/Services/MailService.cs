using ECommerce.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ECommerce.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMessageAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mailMessage = new()
            {
                IsBodyHtml = isBodyHtml,
                Subject = subject,
                Body = body,
                From = new MailAddress(_configuration["Mail:Username"], "ECommerce", System.Text.Encoding.UTF8)
            };

            foreach (var to in tos)
                mailMessage.To.Add(to);

            SmtpClient smtp = new()
            {
                Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]),
                Host = _configuration["Mail:Host"],
                Port = Convert.ToInt32(_configuration["Mail:Port"]),
                EnableSsl = true,
            };

            await smtp.SendMailAsync(mailMessage);
        }
    }
}
