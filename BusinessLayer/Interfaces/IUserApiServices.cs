using EntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserApiServices
    {
        Task<bool> RegisterUserAsync(RegisterUserDTO dto);
        Task<bool> LoginUserAsync(string username, string password);
        Task<bool> LogoutUserAsync();
        Task<bool> UpdateUserProfileAsync(string username, string newPassword);
        Task<bool> DeleteUserAccountAsync(string username);
    }
}