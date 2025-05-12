using BusinessLayer.Interfaces;
using EntityLayer.DTOs;
using EntityLayer.Entitys;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
        public async Task<IActionResult> GetAppointment(int? cityId)
        {
            
            List<Citys>? cities = await _cityService.GetCitiesAsync();
            var pitches = await _pitchService.GetPitchesAsync();

            if (cityId.HasValue)
            {
                pitches = pitches.Where(p => p.CitysId == cityId.Value).ToList();
            }

            ViewBag.Cities = cities;
            ViewBag.Pitches = pitches;

            return View(new AppointmentDTO());
        }

        private int GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                return int.Parse(userIdClaim.Value);
            }
            throw new Exception("Kullanıcı ID'si bulunamadı");
        }

        [HttpPost]
        public async Task<IActionResult> GetAppointment(AppointmentDTO dto)
        {
            if (ModelState.IsValid)
            {
                var timeStart = dto.SelectedTime.Split('-')[0].Trim();
                var time = TimeSpan.Parse(timeStart);
                dto.Date = dto.Date.Date.Add(time);

                string token = HttpContext.Session.GetString("Token");
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "LoginPage");
                }
                dto.UserId = GetUserIdFromToken(token);
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

        [HttpGet]
        public async Task<IActionResult> GetPitchesByCity(int cityId)
        {
            var pitches = await _pitchService.GetPitchesAsync();
            var filteredPitches = pitches.Where(p => p.CitysId == cityId).ToList();

            return PartialView("_PitchListPartial", filteredPitches);
        }
    }
}