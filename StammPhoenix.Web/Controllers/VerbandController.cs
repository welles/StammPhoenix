using Microsoft.AspNetCore.Mvc;

namespace StammPhoenix.Web.Controllers
{
    public class VerbandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
