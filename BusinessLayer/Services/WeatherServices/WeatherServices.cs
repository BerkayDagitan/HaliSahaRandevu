using BusinessLayer.Interfaces.WeatherInfo;
using EntityLayer.DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

public class WeatherService : IWeatherService
{
    private readonly string _apiKey;

    public WeatherService(IConfiguration configuration)
    {
        _apiKey = configuration["WeatherApi:ApiKey"];
    }

    public async Task<List<WeatherInfoDTO>> GetWeatherForecastsAsync(string city)
    {
        var forecasts = new List<WeatherInfoDTO>();
        try
        {
            string url = $"https://api.openweathermap.org/data/2.5/forecast?q={city},tr&appid={_apiKey}&units=metric&lang=tr";
            using var client = new HttpClient();
            var response = await client.GetStringAsync(url);
            JObject json = JObject.Parse(response);

            var list = json["list"];
            var dates = new HashSet<string>();
            foreach (var item in list)
            {
                string date = ((string)item["dt_txt"]).Split(' ')[0];
                if (!dates.Contains(date))
                {
                    dates.Add(date);
                    forecasts.Add(new WeatherInfoDTO
                    {
                        Date = date,
                        Temp = (double)item["main"]["temp"],
                        Description = (string)item["weather"][0]["description"],
                        Icon = (string)item["weather"][0]["icon"]
                    });
                }
                if (forecasts.Count == 3) break;
            }
        }
        catch (Exception ex)
        {
            // Hata mesajını logla veya gerekirse başka bir şekilde ilet
            Console.WriteLine($"Weather API error for {city}: {ex.Message}");
        }
        return forecasts;
    }
}