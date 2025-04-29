using BusinessLayer.Interfaces;
using EntityLayer.DTOs;
using EntityLayer.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentApiServices _service;
        private readonly IPitchApiServices _pitchService;
        private readonly ICityApiServices _cityService;

        public AppointmentController(IAppointmentApiServices service, IPitchApiServices pitchService, ICityApiServices cityService)
        {
            _service = service;
            _pitchService = pitchService;
            _cityService = cityService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointment()
        {
            List<Citys>? cities = await _cityService.GetCitiesAsync();
            var pitches = await _pitchService.GetPitchesAsync();

            ViewBag.Cities = cities;
            ViewBag.Pitches = pitches;

            return View(new AppointmentDTO());
        }

        [HttpPost]
        public async Task<IActionResult> GetAppointment(AppointmentDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateAppointmentAsync(dto);
                if (result)
                {
                    TempData["Success"] = "Randevu başarıyla alındı!";
                    return RedirectToAction("Home", "HomePage");
                }
                else
                {
                    TempData["Error"] = "Randevu alınamadı!";
                }
            }
            ViewBag.Cities = await _cityService.GetCitiesAsync();
            ViewBag.Pitches = await _pitchService.GetPitchesAsync();
            return View(dto);
        }
    }
}