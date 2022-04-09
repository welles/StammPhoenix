using System.Security.Claims;
using StammPhoenix.Persistence.Models;

namespace StammPhoenix.Web.Extensions;

public static class IdentityExtensions
{
    public static bool IsAuthenticated(this HttpContext context)
    {
        return context.User.Identity is ClaimsIdentity { IsAuthenticated: true };
    }

    public static ClaimsIdentity GetUser(this HttpContext context)
    {
        if (context.User.Identity is not ClaimsIdentity identity)
        {
            throw new InvalidOperationException("Current Identity is not set!");
        }

        return identity;
    }

    public static string GetUserEmail(this HttpContext context)
    {
        if (context.User.Identity is not ClaimsIdentity identity)
        {
            throw new InvalidOperationException("Current Identity is not set!");
        }

        var email = identity.FindFirst(ClaimTypes.Email)?.Value ?? throw new InvalidOperationException("Current Identity Email is not set!");

        return email;
    }


    public static string GetUserId(this HttpContext context)
    {
        if (context.User.Identity is not ClaimsIdentity identity)
        {
            throw new InvalidOperationException("Current Identity is not set!");
        }

        return identity.Name ?? throw new InvalidOperationException("Current Identity ID is not set!");
    }

    public static string GetUserGivenName(this HttpContext context)
    {
        var identity = context.GetUser();

        return identity.Claims.Single(x => x.Type == ClaimTypes.GivenName).Value;
    }

    public static bool NeedsPasswordChange(this ClaimsIdentity identity)
    {
        var claim = identity.Claims.SingleOrDefault(x => x.Type.Equals(nameof(LoginUser.NeedPasswordChange)));

        var needsPasswordChange = claim != null && bool.Parse(claim.Value);

        return needsPasswordChange;
    }
}
