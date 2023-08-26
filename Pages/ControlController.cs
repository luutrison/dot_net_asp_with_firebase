using Microsoft.AspNetCore.Mvc;

namespace BAN_BANH.Pages
{
    public class ControlController : Controller
    {
        public IActionResult Product()
        {
            return View();
        }

        public IActionResult Index() {
            var param = RouteData.Values["id"];
            return View();
        }
    }
}
