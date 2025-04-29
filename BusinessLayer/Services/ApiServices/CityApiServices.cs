using BusinessLayer.Interfaces;
using EntityLayer.Entitys;
using System.Net.Http.Json;

namespace BusinessLayer.Services.ApiServices
{
    public class CityApiServices : ICityApiServices
    {
        private readonly HttpClient _httpClient;

        public CityApiServices(HttpClient httpClient)
        {
            if (httpClient.BaseAddress == null)
            {
                httpClient.BaseAddress = new Uri("http://localhost:5138/api/");
            }
            _httpClient = httpClient;
        }

        public async Task<List<Citys>> GetCitiesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Citys>>("city/list");

                if (response == null)
                {
                    return new List<Citys>();
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCitiesAsync: {ex.Message}");
                return new List<Citys>();
            }
        }
    }
}