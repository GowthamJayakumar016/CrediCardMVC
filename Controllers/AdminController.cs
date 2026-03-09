using CreditCardAppMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardAppMvc.Controllers
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

        public async Task<IActionResult> Applications()
        {
            var apps = await _service.GetAllApplications();
            return View(apps);
        }

        public async Task<IActionResult> Pending()
        {
            var apps = await _service.GetApplicationsByStatus("Pending");
            return View("Applications", apps);
        }

        public async Task<IActionResult> Approved()
        {
            var apps = await _service.GetApplicationsByStatus("Approved");
            return View("Applications", apps);
        }

        public async Task<IActionResult> Rejected()
        {
            var apps = await _service.GetApplicationsByStatus("Rejected");
            return View("Applications", apps);
        }

        public async Task<IActionResult> Approve(int id)
        {
            await _service.ApproveApplication(id);
            return RedirectToAction("Pending");
        }

        public async Task<IActionResult> Reject(int id)
        {
            await _service.RejectApplication(id);
            return RedirectToAction("Pending");
        }
    }
}