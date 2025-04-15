using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
