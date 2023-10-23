using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
