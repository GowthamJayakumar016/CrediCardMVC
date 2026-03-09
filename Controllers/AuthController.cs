using CreditCardAppMvc.DTOs;
using CreditCardAppMvc.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CreditCardAppMvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                await _service.Register(dto);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var result = await _service.Login(dto);

                Response.Cookies.Append("jwt", result.token);

                if (result.role == "Admin")
                    return RedirectToAction("Dashboard", "Admin");

                return RedirectToAction("Dashboard", "User");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }
    }
}