using System.Security.Claims;
using System.Security.Principal;

namespace StammPhoenix.Web.Extensions;

public static class IdentityExtensions
{
    public static string GetGivenName(this IIdentity? identity)
    {
        var claimsIdentity = identity as ClaimsIdentity;

        if (claimsIdentity == null)
        {
            throw new ArgumentNullException(nameof(identity));
        }

        return claimsIdentity.Claims.Single(x => x.Type == ClaimTypes.GivenName).Value;
    }
}