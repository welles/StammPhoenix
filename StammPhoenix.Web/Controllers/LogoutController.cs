using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StammPhoenix.Web.Extensions;

namespace StammPhoenix.Web.Controllers;

[Authorize]
public class LogoutController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return this.RedirectTo("Index", "Home");
    }
}
