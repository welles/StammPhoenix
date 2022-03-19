using Microsoft.AspNetCore.Mvc;

namespace StammPhoenix.Web.Areas.Leiter.Controllers
{
    [Area("Leiter")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}