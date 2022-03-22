using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StammPhoenix.Web.Controllers;

[Authorize]
public class LogoutController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return this.RedirectToAction("Index", "Home");
    }
}