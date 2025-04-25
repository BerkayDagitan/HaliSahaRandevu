using AspNetCoreGeneratedDocument;
using BusinessLayer.Interfaces;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class LoginPageController : Controller
    {
        private readonly IUserApiServices _service;

        public LoginPageController(IUserApiServices service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserLoginDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _service.LoginUserAsync(username, password);
            if(user == null)
            {
                TempData["Error"] = "Kullanıcı adı veya şifre hatalı!";
                return View();
            }
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
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorRegister"] = "Kayıt başarısız!";
                }
            }
            return View(dto);
        }
    }
}
