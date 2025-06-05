using BusinessLayer.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace BusinessLayer.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailServices> _emailServices;

        public EmailServices(IConfiguration configuration, ILogger<EmailServices> emailServices)
        {
            _configuration = configuration;
            _emailServices = emailServices;
        }

        public async Task SendWelcomeEmailAsync(string to, string firstName)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Halı Saha Randevu", _configuration["EmailSettings:From"]));
                email.To.Add(new MailboxAddress(firstName, to));
                email.Subject = "Halı Saha Randevu Sistemine Hoş Geldiniz!";

                var builder = new BodyBuilder();
                builder.HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                    <h2 style='color: #4361ee;'>Hoş Geldiniz {firstName}!</h2>
                    <p>Halı Saha Randevu sistemine kayıt olduğunuz için teşekkür ederiz.</p>
                    <p>Artık aşağıdaki özellikleri kullanabilirsiniz:</p>
                    <ul>
                        <li>Halı saha rezervasyonu yapma</li>
                        <li>Rezervasyonlarınızı görüntüleme</li>
                        <li>Rezervasyonlarınızı yönetme</li>
                    </ul>
                    <p>Herhangi bir sorunuz olursa bizimle iletişime geçebilirsiniz.</p>
                    <p style='color: #666; font-size: 12px;'>Bu e-posta otomatik olarak gönderilmiştir, lütfen yanıtlamayınız.</p>
                </div>";

                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(
                    _configuration["EmailSettings:SmtpServer"],
                    int.Parse(_configuration["EmailSettings:Port"]),
                    SecureSocketOptions.StartTls
                );

                await smtp.AuthenticateAsync(
                    _configuration["EmailSettings:Username"],
                    _configuration["EmailSettings:Password"]
                );

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _emailServices.LogError(ex, "E-posta gönderilirken hata oluştu");
                throw;
            }
        }
    }
}
