using Microsoft.AspNetCore.Mvc;

namespace StammPhoenix.Web.Controllers
{
    public class DownloadsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
