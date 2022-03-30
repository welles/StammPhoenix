using Microsoft.AspNetCore.Mvc;

namespace StammPhoenix.Web.Controllers
{
    public class StufeController : Controller
    {
        [HttpGet]
        public IActionResult Woelflinge()
        {
            return this.View("~/Views/Stufe/Woelflinge.cshtml");
        }

        [HttpGet]
        public IActionResult Jungpfadfinder()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Pfadfinder()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Rover()
        {
            return this.View();
        }
    }
}
