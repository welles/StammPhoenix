using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StammPhoenix.Web.Models.Home;

namespace StammPhoenix.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Exception()
        {
            throw new NotSupportedException("This is a test error. You should not be here!");
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string? id = null)
        {
            var viewModel = new ErrorViewModel();

            if (int.TryParse(id, out var statusCode))
            {
                viewModel.StatusCode = statusCode;
            }

            var exceptionFeature = this.HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                viewModel.Error = exceptionFeature.Error;
            }

            return this.View(viewModel);
        }
    }
}
