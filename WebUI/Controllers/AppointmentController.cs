using BusinessLayer.Interfaces;
using EntityLayer.DTOs;
using EntityLayer.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentApiServices _appointmentService;
        private readonly IPitchApiServices _pitchService;
        private readonly ICityApiServices _cityService;

        public AppointmentController(IAppointmentApiServices appointmentService, IPitchApiServices pitchService, ICityApiServices cityService)
        {
            _appointmentService = appointmentService;
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
                var timeStart = dto.SelectedTime.Split('-')[0].Trim();
                var time = TimeSpan.Parse(timeStart);

                dto.Date = dto.Date.Date.Add(time);

                dto.UserId = 1;
                var result = await _appointmentService.CreateAppointmentAsync(dto);
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

        [HttpGet]
        public async Task<IActionResult> ListAppointment()
        {
            var appointments = await _appointmentService.AppointmentListAsync();

            return View(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var result = await _appointmentService.DeleteAppointmentAsync(id);
            if (result)
            {
                TempData["Success"] = "Randevu başarıyla silindi.";
            }
            else
            {
                TempData["Error"] = "Randevu silinirken bir hata oluştu.";
            }

            return RedirectToAction("ListAppointment");
        }
    }
}