using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    public class PermissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
