using System.Web;
using StammPhoenix.Util.Interfaces;

namespace StammPhoenix.Web.Core;

public class NeedsPasswordChangeMiddleware : IMiddleware
{
    private IAuth Auth { get; }

    public NeedsPasswordChangeMiddleware(IAuth auth)
    {
        this.Auth = auth;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (this.Auth.IsAuthenticated() == true &&
            context.Request.Path != new PathString("/leiter/konto/changepassword") &&
            context.Request.Path != new PathString("/logout") &&
            this.Auth.GetUserNeedsPasswordChange() == true)
        {
            var redirect = context.Request.Path.Value == "/" ? "" : "?redirect=" + HttpUtility.UrlEncode(context.Request.Path.Value);
            context.Response.Redirect("/leiter/konto/changepassword" + redirect);
        }

        await next(context).ConfigureAwait(true);
    }
}
