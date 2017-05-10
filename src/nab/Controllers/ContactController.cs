using Microsoft.AspNetCore.Mvc;
using nab.Models;
using nab.Services;
using System.Threading.Tasks;

namespace nab.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEmailService _emailService;

        public ContactController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        
        public IActionResult Index()
        {
            ViewData["success"] = false;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Email(UserEmail model)
        {
            if (ModelState.IsValid)
            {
                await _emailService.SendEmailAsync(model.SenderName, model.SenderEmailAddress.Trim(), model.EmailSubject, model.EmailMessage);

                // check here if success and then
                ViewData["success"] = true;
            }
            else
            {
                ViewData["success"] = false;
            }

            return View("Index");
        }
    }
}
