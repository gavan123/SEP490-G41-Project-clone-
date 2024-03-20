using Microsoft.AspNetCore.Mvc;

namespace AR_NavigationAPI.Controllers
{
    public class BuildingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
