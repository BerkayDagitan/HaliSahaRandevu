using BusinessLayer.Interfaces;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentApiServices _service;
        public AppointmentController(IAppointmentApiServices service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointment()
        {
            var cities = await _service.AppointmentListAsync();
            ViewBag.Cities = cities;

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
                    return RedirectToAction("Home","HomePage");
                }
                else
                {
                    TempData["Error"] = "Randevu alınamadı!";
                }
            }
            return View(dto);
        }
    }
}