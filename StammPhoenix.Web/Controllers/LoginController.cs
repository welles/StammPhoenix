using Microsoft.AspNetCore.Mvc;

namespace StammPhoenix.Web.Controllers;

public class LoginController : Controller
{
    public IActionResult Index()
    {
        return this.View();
    }
}