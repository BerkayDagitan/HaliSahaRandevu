using BusinessLayer.Interfaces;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BusinessLayer.Services.ApiServices
{
    public class UserApiServices : IUserApiServices
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserApiServices(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            if (httpClient.BaseAddress == null)
            {
                httpClient.BaseAddress = new Uri("http://localhost:5138/api/");
                Console.WriteLine("set edildi: " + httpClient.BaseAddress);
            }
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserLoginDTO> LoginUserAsync(string username, string password)
        {
            var loginInfo = new
            {
                UserName = username,
                Password = password
            };

            var result = await _httpClient.PostAsJsonAsync("User/login", loginInfo);
            if (result.IsSuccessStatusCode)
            {
                var resultString = await result.Content.ReadAsStringAsync();
                dynamic json = JsonConvert.DeserializeObject(resultString);
                string userName = json.user.userName;
                string pass = json.user.password;

                if (userName == username && pass == password)
                {
                    return new UserLoginDTO { UserName = userName, Password = pass };
                }
                else
                {
                    throw new Exception("Kullanıcı bulunamadı.");
                }
            }
            return null;
        }

        public async Task<bool> LogoutUserAsync()
        {
            try
            {
                await _httpContextAccessor.HttpContext.SignOutAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RegisterUserAsync(RegisterUserDTO dto)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = await _httpClient.PostAsync("User/register", content);

            return result.IsSuccessStatusCode;
        }
    }
}