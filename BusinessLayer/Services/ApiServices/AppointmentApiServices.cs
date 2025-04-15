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
            _httpClient = httpClient;
        }

        public async Task<bool> CreateAppointmentAsync(AppointmentDTO dto)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = await _httpClient.PostAsync("Appointment/Create", content);

            if(result.IsSuccessStatusCode is true)
                return result.Content.ReadAsStringAsync().Result == "Randevu Eklendi." ? true : false;
            return false;
        }
        public async Task<List<Appointment>> AppointmentListAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Appointment>>("appointments");

            return response ?? new List<Appointment>();
        }
    }
}
