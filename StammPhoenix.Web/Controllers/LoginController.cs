using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StammPhoenix.Persistence;
using StammPhoenix.Persistence.Models;
using StammPhoenix.Util.Interfaces;
using StammPhoenix.Util.Models;
using StammPhoenix.Web.Core;
using StammPhoenix.Web.Extensions;
using StammPhoenix.Web.Models.Login;

namespace StammPhoenix.Web.Controllers;

public class LoginController : Controller
{
    private IDatabaseContext DatabaseContext { get; }

    private IPasswordHasher PasswordHasher { get; }

    private ITempCookieService TempCookieService { get; }

    private IAuth Auth { get; }

    public LoginController(IDatabaseContext databaseContext, IPasswordHasher passwordHasher, ITempCookieService tempCookieService, IAuth auth)
    {
        this.DatabaseContext = databaseContext;
        this.PasswordHasher = passwordHasher;
        this.TempCookieService = tempCookieService;
        this.Auth = auth;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if (this.Auth.IsAuthenticated())
        {
            return this.RedirectTo("Index", "Home", "Leiter");
        }

        return this.View();
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return this.View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(LoginModel form)
    {
        if (string.IsNullOrWhiteSpace(form.Email))
        {
            this.TempCookieService.SetTempCookie("LoginErrorMessage", "E-Mail-Adresse darf nicht leer sein.");

            return this.RedirectTo("Index", "Login", redirect: form.Redirect);
        }

        if (string.IsNullOrWhiteSpace(form.Password))
        {
            this.TempCookieService.SetTempCookie("LoginErrorMessage", "Passwort darf nicht leer sein.");

            return this.RedirectTo("Index", "Login", redirect: form.Redirect);
        }

        var user = await this.DatabaseContext.FindUserByEmail(form.Email);

        if (user == null)
        {
            this.TempCookieService.SetTempCookie("LoginErrorMessage", "Es existiert kein Benutzer mit diesem Benutzernamen.");

            return this.RedirectTo("Index", "Login", redirect: form.Redirect);
        }

        if (user.IsLocked)
        {
            this.TempCookieService.SetTempCookie("LoginErrorMessage", "Dieser Benutzer ist gesperrt. Bitte kontaktieren Sie den Administrator.");

            return this.RedirectTo("Index", "Login", redirect: form.Redirect);
        }

        var passwordCorrect = this.PasswordHasher.VerifyHashedPassword(user.PasswordHash, form.Password);

        if (passwordCorrect == PasswordVerificationResult.Failed)
        {
            this.TempCookieService.SetTempCookie("LoginErrorMessage", "Das Passwort ist nicht korrekt.");

            return this.RedirectTo("Index", "Login", redirect: form.Redirect);
        }

        if (passwordCorrect == PasswordVerificationResult.SuccessRehashNeeded)
        {
            var newHash = this.PasswordHasher.HashPassword(form.Password);

            await this.DatabaseContext.ChangeUserPassword(user, newHash);
        }

        var securityStamp = Guid.NewGuid();

        await this.DatabaseContext.ChangeUserSecurityStamp(user, securityStamp);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(CustomClaimTypes.SecurityStamp, securityStamp.ToString()),
            new Claim(CustomClaimTypes.UserNeedsPasswordChange, user.NeedPasswordChange.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            //AllowRefresh = <bool>,
            // Refreshing the authentication session should be allowed.

            //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            // The time at which the authentication ticket expires. A
            // value set here overrides the ExpireTimeSpan option of
            // CookieAuthenticationOptions set with AddCookie.

            IsPersistent = form.IsPersistent,
            // Whether the authentication session is persisted across
            // multiple requests. When used with cookies, controls
            // whether the cookie's lifetime is absolute (matching the
            // lifetime of the authentication ticket) or session-based.

            //IssuedUtc = <DateTimeOffset>,
            // The time at which the authentication ticket was issued.

            //RedirectUri = <string>
            // The full path or absolute URI to be used as an http
            // redirect response value.
        };

        await this.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        if (!string.IsNullOrWhiteSpace(form.Redirect) && this.Url.IsLocalUrl(form.Redirect))
        {
            return this.Redirect(form.Redirect);
        }

        return this.RedirectTo("Index", "Home", "Leiter");
    }
}
