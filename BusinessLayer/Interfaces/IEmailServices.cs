namespace BusinessLayer.Interfaces
{
    public interface IEmailServices
    {
        Task SendWelcomeEmailAsync(string to, string firstName);
    }
}