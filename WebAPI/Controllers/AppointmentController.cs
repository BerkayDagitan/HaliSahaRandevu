using BusinessLayer.Interfaces;
using DataAccessLayer.Context;
using EntityLayer.DTOs;
using EntityLayer.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ProjectContext _db;
        private readonly IAppointmentApiServices _services;

        public AppointmentController(IAppointmentApiServices services, ProjectContext db)
        {
            _services = services;
            _db = db;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDTO dto)
        {
            Appointment appointment = new Appointment()
            {
                UserId = dto.UserId,
                Date = dto.Date,
                CitysId = dto.CitysId,
                PitchId = dto.PitchId
            };
            await _db.Appointments.AddAsync(appointment);
            var saved = await _db.SaveChangesAsync();
            return saved > 0 ? Ok("Randevu Eklendi.") : BadRequest("Randevu Eklenemedi.");
        }

        [HttpGet("List")]
        public async Task<IActionResult> ListAppointment(int weekOffset = 0)
        {
            var result = await Task.FromResult(_db.Appointments.ToList());
            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            return NotFound("");




            //if (weekOffset < -2) weekOffset = -2;
            //if (weekOffset > 2) weekOffset = 2;

            //DateTime today = DateTime.Today;
            //DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday).Date;
            //startOfWeek = startOfWeek.AddDays(weekOffset * 7);

            //var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (userIdString == null)
            //{
            //    return Unauthorized();
            //}

            //int currentUserId = int.Parse(userIdString);

            //var userAppointments = _db.Appointments
            //    .Where(a => a.UserId == currentUserId && a.Date >= startOfWeek && a.Date < startOfWeek.AddDays(7))
            //    .ToList();

            //var slots = new List<AppointmentSlot>();
            //for (int day = 0; day < 7; day++)
            //{
            //    for (int hour = 9; hour <= 23; hour++)
            //    {
            //        var slotDate = startOfWeek.AddDays(day).AddHours(hour);
            //        var isBooked = userAppointments.Any(a => a.Date == slotDate);
            //        slots.Add(new AppointmentSlot
            //        {
            //            DateTime = slotDate,
            //            IsBooked = isBooked
            //        });
            //    }
            //}

            //var model = new WeeklyCalendarDTO
            //{
            //    StartOfWeek = startOfWeek,
            //    Slots = slots,
            //    WeekOffset = weekOffset
            //};

            //return Ok(model);
        }
    }
}