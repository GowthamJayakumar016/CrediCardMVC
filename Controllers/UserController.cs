using CreditCardAppMvc.DTOs;
using CreditCardAppMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardAppMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IApplicationService _service;

        public UserController(IApplicationService service)
        {
            _service = service;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Apply(CreditCardApplicationDto dto)
        {
            int userId = 1;

            await _service.Apply(userId, dto);

            return RedirectToAction("Applications");
        }

        public async Task<IActionResult> Applications()
        {
            int userId = 1;

            var apps = await _service.GetUserApplications(userId);

            return View(apps);
        }
    }
}