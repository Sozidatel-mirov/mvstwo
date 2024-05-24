using Microsoft.AspNetCore.Mvc;

namespace mvstwo.Controllers
{
    public class MainPage : Controller
    {
        [HttpPost]
        public IActionResult login()
        {
            return View();
        }
    }
}
