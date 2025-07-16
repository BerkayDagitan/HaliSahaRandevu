using BusinessLayer.Interfaces.Email;
using EntityLayer.DTOs;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace BusinessLayer.Services.EmailServices
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailSettingsDTO _emailServices; 

        public EmailServices(IConfiguration configuration)
        {
            _emailServices = configuration.GetSection("EmailSettings").Get<EmailSettingsDTO>();
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Halı Saha Randevu", _emailServices.From));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(_emailServices.SmtpServer, _emailServices.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailServices.Username, _emailServices.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        public async Task SendWelcomeEmailAsync(WelcomeEmailDTO dto)
        {
            string subject = "Halı Saha Randevu Sistemine Hoşgeldiniz!";
            string body = $"<h2>Merhaba {dto.FirstName},</h2><p>Sisteme kayıt olduğunuz için teşekkürler.</p>";
            await SendEmailAsync(dto.Email, subject, body);
        }
    }
}
