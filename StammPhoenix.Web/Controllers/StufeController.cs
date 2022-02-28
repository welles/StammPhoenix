using Microsoft.AspNetCore.Mvc;

namespace StammPhoenix.Web.Controllers
{
    public class StufeController : Controller
    {
        public IActionResult Woelflinge()
        {
            return this.View("~/Views/Stufe/Woelflinge.cshtml");
        }
        public IActionResult Jungpfadfinder()
        {
            return this.View();
        }
        public IActionResult Pfadfinder()
        {
            return this.View();
        }
        public IActionResult Rover()
        {
            return this.View();
        }
    }
}
