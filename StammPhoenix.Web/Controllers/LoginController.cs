using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StammPhoenix.Persistence;
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

    private IMapper Mapper { get; }

    public LoginController(IDatabaseContext databaseContext, IPasswordHasher passwordHasher, ITempCookieService tempCookieService, IAuth auth, IMapper mapper)
    {
        this.DatabaseContext = databaseContext;
        this.PasswordHasher = passwordHasher;
        this.TempCookieService = tempCookieService;
        this.Auth = auth;
        this.Mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if (this.Auth.IsAuthenticated())
        {
            return this.RedirectTo("Index", "Home", "Leiter");
        }

        var viewModel = new LoginViewModel();

        if (this.TempCookieService.TryGetTempCookie("LoginErrorMessage", out var errorMessage))
        {
            viewModel.ErrorMessage = errorMessage;
        }

        if (this.TempCookieService.TryGetTempCookie("LoginInfoMessage", out var infoMessage))
        {
            viewModel.InfoMessage = infoMessage;
        }

        return this.View(viewModel);
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return this.View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(LoginFormModel form)
    {
        if (string.IsNullOrWhiteSpace(form.Email))
        {
            var viewModel = this.Mapper.Map<LoginViewModel>(form);
            viewModel.ErrorMessage = "E-Mail-Adresse darf nicht leer sein.";

            return this.View(viewModel);
        }

        if (string.IsNullOrWhiteSpace(form.Password))
        {
            var viewModel = this.Mapper.Map<LoginViewModel>(form);
            viewModel.ErrorMessage = "Passwort darf nicht leer sein.";

            return this.View(viewModel);
        }

        var user = await this.DatabaseContext.FindUserByEmail(form.Email);

        if (user == null)
        {
            var viewModel = this.Mapper.Map<LoginViewModel>(form);
            viewModel.ErrorMessage = "Es existiert kein Benutzer mit diesem Benutzernamen.";

            return this.View(viewModel);
        }

        if (user.IsLocked)
        {
            var viewModel = this.Mapper.Map<LoginViewModel>(form);
            viewModel.ErrorMessage = "Dieser Benutzer ist gesperrt. Bitte kontaktieren Sie den Administrator.";

            return this.View(viewModel);
        }

        var passwordCorrect = this.PasswordHasher.VerifyHashedPassword(user.PasswordHash, form.Password);

        if (passwordCorrect == PasswordVerificationResult.Failed)
        {
            var viewModel = this.Mapper.Map<LoginViewModel>(form);
            viewModel.ErrorMessage = "Das Passwort ist nicht korrekt.";

            return this.View(viewModel);
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
