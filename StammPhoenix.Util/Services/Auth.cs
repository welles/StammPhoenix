using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using StammPhoenix.Util.Interfaces;
using StammPhoenix.Util.Models;

namespace StammPhoenix.Util.Services;

public class Auth : IAuth
{
    private IHttpContextAccessor HttpContextAccessor { get; }

    public Auth(IHttpContextAccessor httpContextAccessor)
    {
        this.HttpContextAccessor = httpContextAccessor;
    }

    public bool IsAuthenticated()
    {
        return this.HttpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }

    public Guid? GetUserId()
    {
        var claim =  this.HttpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

        return Guid.TryParse(claim, out var result) ? result : null;
    }

    public string? GetUserEmail()
    {
        return this.HttpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
    }

    public string? GetUserGivenName()
    {
        return this.HttpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.GivenName)?.Value;
    }

    public Guid? GetUserSecurityStamp()
    {
        var claim = this.HttpContextAccessor.HttpContext?.User?.FindFirst(CustomClaimTypes.SecurityStamp)?.Value;

        return Guid.TryParse(claim, out var result) ? result : null;
    }

    public bool GetUserNeedsPasswordChange()
    {
        var claim = this.HttpContextAccessor.HttpContext?.User?.FindFirst(CustomClaimTypes.UserNeedsPasswordChange)?.Value;

        return bool.TryParse(claim, out var result) && result;
    }
}
