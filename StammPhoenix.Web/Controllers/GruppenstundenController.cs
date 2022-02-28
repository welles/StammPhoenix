using Microsoft.AspNetCore.Mvc;

namespace StammPhoenix.Web.Controllers
{
    public class GruppenstundenController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
