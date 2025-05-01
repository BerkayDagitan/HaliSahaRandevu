using BusinessLayer.Interfaces;
using DataAccessLayer.Context;
using EntityLayer.DTOs;
using EntityLayer.Entitys;
using Microsoft.AspNetCore.Mvc;

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
        }
    }
}