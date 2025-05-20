using BusinessLayer.Interfaces;
using EntityLayer.DTOs;
using EntityLayer.Entitys;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services.ApiServices
{
    public class AppointmentApiServices : IAppointmentApiServices
    {
        private readonly HttpClient _httpClient;

        public AppointmentApiServices(HttpClient httpClient)
        {
            if (httpClient.BaseAddress == null)
            {
                httpClient.BaseAddress = new Uri("http://localhost:5138/api/");
            }
            _httpClient = httpClient;
        }

        public async Task<bool> CreateAppointmentAsync(AppointmentDTO dto)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = await _httpClient.PostAsync("Appointment/Create", content);

            if (result.IsSuccessStatusCode is true)
                return result.Content.ReadAsStringAsync().Result == "Randevu Eklendi." ? true : false;
            var errorMessage = await result.Content.ReadAsStringAsync();
            throw new Exception(errorMessage);
        }
        public async Task<List<AppointmentListDTO>> AppointmentListAsync()
        {
            try
            {
                var httpContextAccessor = new HttpContextAccessor();
                string token = httpContextAccessor.HttpContext?.Session.GetString("Token");

                var request = new HttpRequestMessage(HttpMethod.Get, "appointment/list");
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<AppointmentListDTO>>(json);
                }
                return new List<AppointmentListDTO>();
            }
            catch
            {
                return new List<AppointmentListDTO>();
            }
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"appointment/delete/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content == "Randevu başarıyla silindi.";
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAppointmentAsync: {ex.Message}");
                return false;
            }
        }
    }
}