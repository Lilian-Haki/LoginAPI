using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
