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

        [HttpPost("Register")]
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

        [HttpPost("Login")]
        public IActionResult PostLogin([FromBody] UserLoginDTO dto)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserName == dto.UserName && x.Password == dto.Password);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("Kullanıcı adı veya şifre hatalı.");
            }
        }
    }
}