using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.API.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
