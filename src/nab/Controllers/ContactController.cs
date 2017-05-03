using Microsoft.AspNetCore.Mvc;

namespace nab.Controllers
{
    public class ContactController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
