using CreditCardAppMvc.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CreditCardMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IApplicationService _service;

        public AdminController(IApplicationService service)
        {
            _service = service;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> Applications(string status)
        {
            var apps = await _service.GetAllApplications();

            if (!string.IsNullOrEmpty(status))
                apps = apps.Where(x => x.Status == status).ToList();

            return View(apps);
        }

        public async Task<IActionResult> Approve(int id)
        {
            await _service.Approve(id);
            return RedirectToAction("Applications");
        }

        public async Task<IActionResult> Reject(int id)
        {
            await _service.Reject(id);
            return RedirectToAction("Applications");
        }
    }
}