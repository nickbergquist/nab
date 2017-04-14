using Microsoft.AspNetCore.Mvc;

namespace nab.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
