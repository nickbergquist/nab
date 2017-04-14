using Microsoft.AspNetCore.Mvc;

namespace nab.Controllers
{
    public class WorkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
