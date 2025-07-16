using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.WeatherInfo;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class HomePageController : Controller
    {
        private readonly IUserApiServices _service;
        private readonly IWeatherService _weatherService;

        public HomePageController(IUserApiServices service, IWeatherService weatherService = null)
        {
            _service = service;
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "LoginPage");
        }

        public async Task<IActionResult> Weather()
        {
            var cities = new[] { "Istanbul", "Ankara", "Izmir" };
            var weatherData = new Dictionary<string, List<WeatherInfoDTO>>();
            try
            {
                foreach (var city in cities)
                {
                    weatherData[city] = await _weatherService.GetWeatherForecastsAsync(city);
                }
            }
            catch (Exception ex)
            {
                ViewBag.WeatherError = ex.Message;
            }
            ViewBag.WeatherData = weatherData;
            return View();
        }
    }
}