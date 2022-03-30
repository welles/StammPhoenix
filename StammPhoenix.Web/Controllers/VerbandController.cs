using Microsoft.AspNetCore.Mvc;

namespace StammPhoenix.Web.Controllers
{
    public class VerbandController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
