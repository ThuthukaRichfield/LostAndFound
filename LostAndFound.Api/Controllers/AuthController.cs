using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Api.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
