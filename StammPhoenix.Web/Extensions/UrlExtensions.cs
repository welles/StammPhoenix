using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace StammPhoenix.Web.Extensions;

public static class UrlExtensions
{
    public static string? To(this IUrlHelper urlHelper, [AspMvcAction] string? action,
        [AspMvcController] string? controller, [AspMvcArea] string? area = "", string? redirect = null)
    {
        dynamic routeValuesResult = new { Area = area };
        if (redirect != null)
        {
            routeValuesResult.Redirect = redirect;
        }

        return urlHelper.Action(action, controller, (object)routeValuesResult);
    }

    public static IActionResult RedirectTo(this ControllerBase controllerBase, [AspMvcAction] string? action,
        [AspMvcController] string? controller, [AspMvcArea] string? area = "", string? redirect = null)
    {
        dynamic routeValuesResult = new { Area = area };
        if (redirect != null)
        {
            routeValuesResult.Redirect = redirect;
        }

        return controllerBase.RedirectToAction(action, controller, (object)routeValuesResult);
    }
}
