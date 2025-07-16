using EntityLayer.DTOs;

namespace BusinessLayer.Interfaces.WeatherInfo
{
    public interface IWeatherService
    {
        Task<List<WeatherInfoDTO>> GetWeatherForecastsAsync(string city);
    }
}