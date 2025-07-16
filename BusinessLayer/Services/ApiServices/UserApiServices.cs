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

        public async Task<bool> ChangePasswordAsync(ChangePasswordDTO dto)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = await _httpClient.PostAsJsonAsync("User/change-password", dto);
            return result.IsSuccessStatusCode;
        }

        public async Task<LoginResponseDTO> LoginUserAsync(string username, string password)
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
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(resultString);
                if (loginResponse != null && loginResponse.User != null)
                {
                    return loginResponse;
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
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var result = await _httpClient.PostAsync("User/register", content);

                if (!result.IsSuccessStatusCode)
                {
                    var errorContent = await result.Content.ReadAsStringAsync();
                    throw new Exception(errorContent);
                }

                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}