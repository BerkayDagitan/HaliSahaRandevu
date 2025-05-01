using BusinessLayer.Interfaces;
using DataAccessLayer.Context;
using EntityLayer.DTOs;
using EntityLayer.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var result = await Task.FromResult(_db.Appointments.Include(x => x.Pitch).Include(x => x.Citys).Select(x => new AppointmentListDTO
            {
                Id = x.Id,
                AppointmentDate = x.Date.ToString("dd/MM/yyyy HH:mm"),
                CitysName = x.Citys.Name,
                PitchName = x.Pitch.Name
            }).ToList());
            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            return NotFound("Randevu bulunamadı.");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                var appointment = await _db.Appointments.FindAsync(id);
                if (appointment == null)
                {
                    return NotFound("Randevu bulunamadı.");
                }
                _db.Appointments.Remove(appointment);
                var result = await _db.SaveChangesAsync();
                
                return result > 0 
                    ? Ok("Randevu başarıyla silindi.") 
                    : BadRequest("Randevu silinirken bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }
    }
}