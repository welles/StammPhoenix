using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using StammPhoenix.Persistence;
using StammPhoenix.Util.Interfaces;

namespace StammPhoenix.Web.Core;

public class CheckSecurityStampMiddleware : IMiddleware
{
    private IAuth Auth { get; }

    private IDatabaseContext DatabaseContext { get; }

    private ITempCookieService TempCookieService { get; }

    public CheckSecurityStampMiddleware(IAuth auth, IDatabaseContext databaseContext, ITempCookieService tempCookieService)
    {
        this.Auth = auth;
        this.DatabaseContext = databaseContext;
        this.TempCookieService = tempCookieService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (this.Auth.IsAuthenticated())
        {
            var userId = this.Auth.GetUserId();
            var securityStamp = this.Auth.GetUserSecurityStamp();

            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (securityStamp == null)
            {
                throw new ArgumentNullException(nameof(securityStamp));
            }

            if (!await this.DatabaseContext.VerifyUserSecurityStamp(userId.Value, securityStamp.Value))
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var redirect = context.Request.Path.Value == "/"
                    ? ""
                    : "?redirect=" + HttpUtility.UrlEncode(context.Request.Path.Value);
                context.Response.Redirect("/login" + redirect);
                this.TempCookieService.SetTempCookie("LoginErrorMessage",
                    "Dieses Konto wurde an einem anderen Gerät angemeldet. Bitte melden Sie sich neu an.");
            }
        }

        await next(context).ConfigureAwait(true);
    }
}
