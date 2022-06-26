using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StammPhoenix.Web.Areas.Leiter.Controllers;

[Authorize]
[Area("Leiter")]
public class TeamsController : Controller
{
    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        return this.View();
    }
}
