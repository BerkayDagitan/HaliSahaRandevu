using EntityLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IUserApiServices
    {
        Task<bool> RegisterUserAsync(RegisterUserDTO dto);
        Task<UserLoginDTO> LoginUserAsync(string username, string password);
        Task<bool> LogoutUserAsync();
    }
}