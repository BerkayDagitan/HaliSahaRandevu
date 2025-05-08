using EntityLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IUserApiServices
    {
        Task<bool> RegisterUserAsync(RegisterUserDTO dto);
        Task<LoginResponseDTO> LoginUserAsync(string username, string password);
        Task<bool> LogoutUserAsync();
    }
}