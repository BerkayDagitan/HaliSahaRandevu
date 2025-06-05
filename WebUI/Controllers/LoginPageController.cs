using AspNetCoreGeneratedDocument;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Token;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class LoginPageController : Controller
    {
        private readonly IUserApiServices _service;
        private readonly ITokenService _tokenService;

        public LoginPageController(IUserApiServices service, ITokenService tokenService)
        {
            _service = service;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserLoginDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var loginResponse = await _service.LoginUserAsync(username, password);
            if(loginResponse == null)
            {
                TempData["Error"] = "Kullanıcı adı veya şifre hatalı!";
                return View();
            }

            HttpContext.Session.SetString("Token", loginResponse.Token);

            return RedirectToAction("Home","HomePage");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterUserDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.RegisterUserAsync(dto);
                if (result)
                {
                    TempData["SuccessRegister"] = "Kayıt başarılı!";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["ErrorRegister"] = "Kayıt başarısız!";
                }
            }
            return View(dto);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordDTO());
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.ChangePasswordAsync(dto);
                if (result)
                {
                    TempData["SuccessChangePassword"] = "Şifre değişikliği başarılı!";
                    return RedirectToAction("Home", "HomePage");
                }
                else
                {
                    TempData["ErrorChangePassword"] = "Şifre değişikliği başarısız!";
                }
            }
            return View(dto);
        }
    }
}