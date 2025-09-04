using EntityLayer.DTOs;

namespace BusinessLayer.Interfaces.Email
{
    public interface IEmailServices
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendWelcomeEmailAsync(WelcomeEmailDTO dto);
    }
}
