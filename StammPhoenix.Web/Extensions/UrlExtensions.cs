using System.Dynamic;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace StammPhoenix.Web.Extensions;

public static class UrlExtensions
{
    public static string? To(this IUrlHelper urlHelper, [AspMvcAction] string? action,
        [AspMvcController] string? controller, [AspMvcArea] string? area = null, string? redirect = null,
        dynamic? routeValues = null)
    {
        dynamic routeValuesResult = new ExpandoObject();
        routeValuesResult.area = area ?? string.Empty;
        if (redirect != null)
        {
            routeValuesResult.redirect = redirect;
        }
        if (routeValues != null)
        {
            routeValues = new RouteValueDictionary(routeValues);
            foreach (var entry in routeValues)
            {
                ((IDictionary<string, object>)routeValuesResult).Add(entry.Key, entry.Value);
            }
        }

        return urlHelper.Action(action, controller, (object)routeValuesResult);
    }

    public static IActionResult RedirectTo(this ControllerBase controllerBase, [AspMvcAction] string? action,
        [AspMvcController] string? controller, [AspMvcArea] string? area = null, string? redirect = null,
        dynamic? routeValues = null)
    {
        dynamic routeValuesResult = new ExpandoObject();
        routeValuesResult.area = area ?? string.Empty;
        if (redirect != null)
        {
            routeValuesResult.redirect = redirect;
        }
        if (routeValues != null)
        {
            routeValues = new RouteValueDictionary(routeValues);
            foreach (var entry in routeValues)
            {
                ((IDictionary<string, object>)routeValuesResult).Add(entry.Key, entry.Value);
            }
        }

        return controllerBase.RedirectToAction(action, controller, (object)routeValuesResult);
    }
}
