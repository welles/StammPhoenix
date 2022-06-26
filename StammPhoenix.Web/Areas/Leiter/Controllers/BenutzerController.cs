using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StammPhoenix.Persistence.Constants;

namespace StammPhoenix.Web.Areas.Leiter.Controllers;

[Authorize(Roles = nameof(Role.Administrator))]
[Area("Leiter")]
public class BenutzerController : Controller
{
    [HttpGet]
    [Authorize(Roles = nameof(Role.Administrator))]
    public IActionResult Index()
    {
        return this.View();
    }
}
