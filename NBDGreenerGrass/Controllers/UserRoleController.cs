using Microsoft.AspNetCore.Mvc;

namespace NBDGreenerGrass.Controllers
{
    public class UserRoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
