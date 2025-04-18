using BusinessLayer.Interfaces;
using DataAccessLayer.Context;
using EntityLayer.DTOs;
using EntityLayer.Entitys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ProjectContext _db;
        private readonly IAppointmentApiServices _services;

        public AppointmentController(IAppointmentApiServices services, ProjectContext db, object httpClient)
        {
            _services = services;
            _db = db;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAppointment([FromBody]AppointmentDTO dto)
        {
            Appointment appointment = new Appointment()
            {
                UserId = dto.UserId,
                Date = dto.Date
            };
            _db.Appointments.AddAsync(appointment);
            return _db.SaveChanges() > 0 ? Ok("Randevu oluşturuldu.") : BadRequest("Randevu oluşturulamadı.");
        }

        [HttpGet("List")]
        public IActionResult ListAppointment()
        {
            return Ok(_db.Appointments.ToList());        }
    }
}