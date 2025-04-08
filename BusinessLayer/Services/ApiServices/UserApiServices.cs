using BusinessLayer.Interfaces;
using EntityLayer.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.ApiServices
{
    public class UserApiServices : IUserApiServices
    {
        private readonly HttpClient _httpClient;

        public UserApiServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<bool> DeleteUserAccountAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoginUserAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LogoutUserAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterUserAsync(RegisterUserDTO dto)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = await _httpClient.PostAsync("User/Register", content);

            if (result.IsSuccessStatusCode is true)
                return result.Content.ReadAsStringAsync().Result == "Üye oluşturuldu." ? true : false;
            return false;
        }

        public Task<bool> UpdateUserProfileAsync(string username, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
