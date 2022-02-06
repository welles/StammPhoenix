using Microsoft.AspNetCore.Mvc;

namespace StammPhoenix.Web.Controllers
{
    public class StufeController : Controller
    {
        public IActionResult Woelflinge()
        {
            return View("~/Views/Stufe/Woelflinge.cshtml");
        }
        public IActionResult Jungpfadfinder()
        {
            return View();
        }
        public IActionResult Pfadfinder()
        {
            return View();
        }
        public IActionResult Rover()
        {
            return View();
        }
    }
}
