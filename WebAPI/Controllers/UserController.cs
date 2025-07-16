using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Token;
using DataAccessLayer.Context;
using EntityLayer.DTOs;
using EntityLayer.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ProjectContext _db;
        private readonly IUserApiServices _services;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;

        public UserController(IUserApiServices userApiServices, ProjectContext context, ITokenService tokenService, IPasswordHasher passwordHasher)
        {
            _services = userApiServices;
            _db = context;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> PostRegister([FromBody] RegisterUserDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Kullanıcı bilgileri eksik.");
            }

            if (await _db.Users.AnyAsync(u => u.UserName.ToLower() == dto.UserName.ToLower()))
            {
                return BadRequest("Bu kullanıcı adı zaten kullanılıyor.");
            }

            if (await _db.Users.AnyAsync(u => u.Email.ToLower().Trim() == dto.Email.ToLower().Trim()))
            {
                return BadRequest("Bu email adresi zaten kullanılıyor.");
            }

            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 10)
            {
                return BadRequest("Şifre en az 10 karakter olmalı.");
            }

            if (dto.Email == null)
            {
                return BadRequest("Email adresi boş olamaz.");
            }

            string hashedPassword = _passwordHasher.HashPassword(dto.Password);

            User user = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.UserName,
                Password = hashedPassword,
                Email = dto.Email
            };

            try
            {
                await _db.Users.AddAsync(user);
                var result = await _db.SaveChangesAsync();
                if (result > 0)
                {
                    return Ok("Kayıt Başarılı");
                }
                else
                {
                    return BadRequest("Kayıt Başarısız");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Kayıt sırasında hata oluştu: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> PostLogin([FromBody] UserLoginDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Kullanıcı adı veya şifre boş olamaz.");
            }

            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == dto.UserName.ToLower());

            if (user == null || !_passwordHasher.VerifyPassword(dto.Password, user.Password))
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
                    user.FirstName,
                    user.LastName
                }
            });
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return Unauthorized("Geçersiz veya eksik token.");
                }

                var token = authHeader.Replace("Bearer ", "");
                int userId;
                try
                {
                    userId = _tokenService.GetUserIdFromToken(token);
                }
                catch
                {
                    return Unauthorized("Geçersiz token.");
                }

                var user = await _db.Users.FindAsync(userId);
                if (user == null)
                {
                    return NotFound("Kullanıcı bulunamadı.");
                }

                if (!_passwordHasher.VerifyPassword(dto.CurrentPassword, user.Password))
                {
                    return BadRequest("Mevcut şifre yanlış.");
                }

                user.Password = _passwordHasher.HashPassword(dto.NewPassword);
                await _db.SaveChangesAsync();
                return Ok("Şifre başarıyla değiştirildi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Şifre değiştirme sırasında bir hata oluştu: {ex.Message}");
            }
        }
    }
}