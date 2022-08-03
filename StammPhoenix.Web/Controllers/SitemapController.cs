using System.Reflection;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StammPhoenix.Web.Extensions;

namespace StammPhoenix.Web.Controllers;

public class SitemapController : Controller
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
        XNamespace xnamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";
        var date = Assembly.GetExecutingAssembly().GetBuildDate();//.ToString("dd.MM.yyyy HH:mm:ss");

        var nodes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(Controller).IsAssignableFrom(t)
                        && t.Namespace == "StammPhoenix.Web.Controllers"
                        && t.GetMethods().Any(m => m.Name == "Index")
                        && !new [] {nameof(LoginController), nameof(LogoutController), nameof(SitemapController)}.Contains(t.Name))
            .Select(x => x.Name.Replace("Controller", string.Empty))
            .Select(x => this.Url.Action("Index", x, null, "https"))
            .Select(x => new XElement(xnamespace + "url",
                new XElement(xnamespace + "loc", x),
                new XElement(xnamespace + "lastmod", date.ToString("yyyy-MM-ddTHH:mm:sszzz")),
                new XElement(xnamespace + "changefreq", "always")))
            .Cast<object>()
            .ToArray();

        var urlset = new XElement(xnamespace + "urlset", nodes);
        var document = new XDocument(urlset);

        return this.Content(document.ToString(), "application/xml");
    }
}
