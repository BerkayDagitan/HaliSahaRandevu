using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Token;
using DataAccessLayer.Context;
using EntityLayer.DTOs;
using EntityLayer.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {   
        private readonly ProjectContext _db;
        private readonly IUserApiServices _services;
        private readonly ITokenService _tokenService;

        public UserController(IUserApiServices userApiServices, ProjectContext context, ITokenService tokenService)
        {
            _services = userApiServices;
            _db = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> PostRegister([FromBody] RegisterUserDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Kullanıcı bilgileri eksik.");
            }
            User user = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.UserName,
                Password = dto.Password
            };
            await _db.Users.AddAsync(user);
            return await _db.SaveChangesAsync() > 0 ? Ok("Kayıt Başarılı") : BadRequest("Kayıt Başarısız");
        }

        [HttpPost("login")]
        public async Task<IActionResult> PostLogin([FromBody] UserLoginDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Kullanıcı adı veya şifre boş olamaz.");
            }
            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == dto.UserName.ToLower() && x.Password == dto.Password);
            if (user == null)
            {
                return NotFound("Kullanıcı adı veya şifreniz hatalı. Tekrar deneyiniz.");
            }
            var token = _tokenService.GenerateJwtToken(user);
            return Ok(new
            {
                token,
                user = new
                {
                    user.UserName,
                    user.Password
                }
            });
        }
    }
}