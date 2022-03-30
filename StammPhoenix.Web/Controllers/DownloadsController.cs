using Microsoft.AspNetCore.Mvc;

namespace StammPhoenix.Web.Controllers
{
    public class DownloadsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
