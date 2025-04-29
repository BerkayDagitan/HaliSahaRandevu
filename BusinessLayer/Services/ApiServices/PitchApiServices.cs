using BusinessLayer.Interfaces;
using EntityLayer.Entitys;
using System.Net.Http.Json;

namespace BusinessLayer.Services.ApiServices
{
    public class PitchApiServices : IPitchApiServices
    {
        private readonly HttpClient _httpClient;

        public PitchApiServices(HttpClient httpClient)
        {
            if (httpClient.BaseAddress == null)
            {
                httpClient.BaseAddress = new Uri("http://localhost:5138/api/");
            }
            _httpClient = httpClient;
        }

        public async Task<List<Pitch>> GetPitchesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Pitch>>("pitch/list");

                if (response == null)
                {
                    return new List<Pitch>();
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetPitchesAsync: {ex.Message}");
                return new List<Pitch>();
            }
        }
    }
}
