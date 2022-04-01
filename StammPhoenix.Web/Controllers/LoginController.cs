using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StammPhoenix.Persistence;
using StammPhoenix.Persistence.Models;
using StammPhoenix.Web.Core;
using StammPhoenix.Web.Extensions;
using StammPhoenix.Web.Models.Login;

namespace StammPhoenix.Web.Controllers;

public class LoginController : Controller
{
    private IDatabaseContext DatabaseContext { get; }

    private IPasswordHasher PasswordHasher { get; }

    private ITempCookieService TempCookieService { get; }

    public LoginController(IDatabaseContext databaseContext, IPasswordHasher passwordHasher, ITempCookieService tempCookieService)
    {
        this.DatabaseContext = databaseContext;
        this.PasswordHasher = passwordHasher;
        this.TempCookieService = tempCookieService;
    }

    [HttpGet]
    public IActionResult Index()
    {
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
            this.TempCookieService.SetTempCookie(this.HttpContext, "LoginErrorMessage", "E-Mail-Adresse darf nicht leer sein.");

            return this.RedirectTo("Index", "Login", redirect: form.Redirect);
        }

        if (string.IsNullOrWhiteSpace(form.Password))
        {
            this.TempCookieService.SetTempCookie(this.HttpContext, "LoginErrorMessage", "Passwort darf nicht leer sein.");

            return this.RedirectTo("Index", "Login", redirect: form.Redirect);
        }

        var user = await this.DatabaseContext.FindUserByEmail(form.Email);

        if (user == null)
        {
            this.TempCookieService.SetTempCookie(this.HttpContext, "LoginErrorMessage", "Es existiert kein Benutzer mit diesem Benutzernamen.");

            return this.RedirectTo("Index", "Login", redirect: form.Redirect);
        }

        if (user.IsLocked)
        {
            this.TempCookieService.SetTempCookie(this.HttpContext, "LoginErrorMessage", "Dieser Benutzer ist gesperrt. Bitte kontaktieren Sie den Administrator.");

            return this.RedirectTo("Index", "Login", redirect: form.Redirect);
        }

        var passwordCorrect = this.PasswordHasher.VerifyHashedPassword(user.PasswordHash, form.Password);

        if (passwordCorrect == PasswordVerificationResult.Failed)
        {
            this.TempCookieService.SetTempCookie(this.HttpContext, "LoginErrorMessage", "Das Passwort ist nicht korrekt.");

            return this.RedirectTo("Index", "Login", redirect: form.Redirect);
        }

        if (passwordCorrect == PasswordVerificationResult.SuccessRehashNeeded)
        {
            var newHash = this.PasswordHasher.HashPassword(form.Password);

            await this.DatabaseContext.ChangeUserPassword(user.Id, newHash);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        if (user.NeedPasswordChange)
        {
            claims.Add(new Claim(nameof(LoginUser.NeedPasswordChange), "true"));
        }

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

    [HttpGet]
    [Authorize]
    public IActionResult ChangePassword()
    {
        return this.View();
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordModel form)
    {
        if (string.IsNullOrWhiteSpace(form.OldPassword))
        {
            this.TempCookieService.SetTempCookie(this.HttpContext, "ChangePasswordErrorMessage", "Das alte Passwort muss angegeben werden.");

            return this.RedirectTo("ChangePassword", "Login", redirect: form.Redirect);
        }

        if (string.IsNullOrWhiteSpace(form.NewPassword))
        {
            this.TempCookieService.SetTempCookie(this.HttpContext, "ChangePasswordErrorMessage", "Das neue Passwort muss angegeben werden.");

            return this.RedirectTo("ChangePassword", "Login", redirect: form.Redirect);
        }

        if (string.IsNullOrWhiteSpace(form.NewPasswordRepeat) || !form.NewPasswordRepeat.Equals(form.NewPassword))
        {
            this.TempCookieService.SetTempCookie(this.HttpContext, "ChangePasswordErrorMessage", "Das Werte für das neue Passwort müssen übereinstimmen.");

            return this.RedirectTo("ChangePassword", "Login", redirect: form.Redirect);
        }

        if (form.NewPassword.Equals(form.OldPassword))
        {
            this.TempCookieService.SetTempCookie(this.HttpContext, "ChangePasswordErrorMessage", "Das neue Passwort muss sich vom alten unterscheiden.");

            return this.RedirectTo("ChangePassword", "Login", redirect: form.Redirect);
        }

        var userId = this.HttpContext.GetUserId();

        var user = await this.DatabaseContext.FindUserById(userId);

        if (user == null)
        {
            this.TempCookieService.SetTempCookie(this.HttpContext, "ChangePasswordErrorMessage", "Es existiert kein Benutzer mit diesem Benutzernamen.");

            return this.RedirectTo("ChangePassword", "Login", redirect: form.Redirect);
        }

        var passwordCorrect = this.PasswordHasher.VerifyHashedPassword(user.PasswordHash, form.OldPassword);

        if (passwordCorrect == PasswordVerificationResult.Failed)
        {
            this.TempCookieService.SetTempCookie(this.HttpContext, "ChangePasswordErrorMessage", "Das alte Passwort ist nicht korrekt.");

            return this.RedirectTo("ChangePassword", "Login", redirect: form.Redirect);
        }

        var newPasswordHash = this.PasswordHasher.HashPassword(form.NewPassword!);

        await this.DatabaseContext.ChangeUserPassword(userId, newPasswordHash);

        await this.DatabaseContext.ChangeUserNeedsPasswordChange(userId, false);

        await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        this.TempCookieService.SetTempCookie(this.HttpContext, "LoginInfoMessage", "Das Passwort wurde erfolgreich geändert. Bitte mit dem neuen Passwort neu anmelden.");

        return this.RedirectTo("Index", "Login", redirect: form.Redirect);
    }
}
