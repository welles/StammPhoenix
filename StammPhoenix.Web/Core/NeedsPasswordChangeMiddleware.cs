using System.Web;
using StammPhoenix.Web.Extensions;

namespace StammPhoenix.Web.Core;

public class NeedsPasswordChangeMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.IsAuthenticated() == true &&
            context.Request.Path != new PathString("/login/changepassword") &&
            context.Request.Path != new PathString("/logout") &&
            context.GetUser().NeedsPasswordChange())
        {
            var redirect = context.Request.Path.Value == "/" ? "" : "?redirect=" + HttpUtility.UrlEncode(context.Request.Path.Value);
            context.Response.Redirect("/login/changepassword" + redirect);
        }
        await next(context).ConfigureAwait(true);
    }
}
