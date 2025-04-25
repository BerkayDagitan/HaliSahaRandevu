using BusinessLayer.Interfaces;
using DataAccessLayer.Context;
using EntityLayer.DTOs;
using EntityLayer.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ProjectContext _db;
        private readonly IUserApiServices _services;

        public UserController(IUserApiServices userApiServices, ProjectContext context)
        {
            _services = userApiServices;
            _db = context;
        }

        [HttpPost("register")]
        public IActionResult PostRegister([FromBody] RegisterUserDTO dto)
        {
            User user = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.UserName,
                Password = dto.Password
            };
            _db.Users.AddAsync(user);
            return _db.SaveChanges() > 0 ? Ok("Kayıt Başarılı") : BadRequest("Kayıt Başarısız");
        }

        [HttpPost("login")]
        public async Task<IActionResult> PostLogin([FromBody] UserLoginDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Kullanıcı adı veya şifre boş olamaz.");
            }

            var user = _db.Users.Where(x => x.UserName.ToLower() == dto.UserName.ToLower() && x.Password == dto.Password).FirstOrDefault();

            if (user == null)
            {
                return NotFound("Kullanıcı adı veya şifreniz hatalı. Tekrar deneyiniz.");
            }
            return Ok(user);
        }
    }
}