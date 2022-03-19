using Microsoft.AspNetCore.Mvc;
using StammPhoenix.Web.Models.Login;

namespace StammPhoenix.Web.Controllers;

public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return this.View();
    }

    [HttpPost]
    public IActionResult Index(LoginModel form)
    {
        return this.View();
    }
}