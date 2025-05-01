using BusinessLayer.Interfaces;
using EntityLayer.DTOs;
using EntityLayer.Entitys;
using Newtonsoft.Json;
using System.Net.Http.Json;

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
            return false;
        }
        public async Task<List<AppointmentListDTO>> AppointmentListAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<AppointmentListDTO>>("appointment/list");
                if (response == null)
                {
                    return new List<AppointmentListDTO>();
                }
                return response;
            }
            catch (Exception ex)
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