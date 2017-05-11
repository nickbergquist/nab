using Microsoft.AspNetCore.Mvc;
using nab.Models.ViewModels;
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
        
        [HttpGet]
        public IActionResult Index(bool success)
        {
            return View(new ContactForm(){ IsSent = false, IsPosted = false });
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                model.IsPosted = true;

                try
                {
                    await _emailService.SendEmailAsync(model.UserEmail.SenderName, model.UserEmail.SenderEmailAddress.Trim(), model.UserEmail.EmailSubject, model.UserEmail.EmailMessage);
                    model.IsSent = true;
                }
                catch
                {
                    model.IsSent = false;
                }
            }

            return View(model);
        }
    }
}
