﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StammPhoenix.Persistence;
using StammPhoenix.Persistence.Models;
using StammPhoenix.Web.Core;
using StammPhoenix.Web.Models.Login;

namespace StammPhoenix.Web.Controllers;

public class LoginController : Controller
{
    private IDatabaseContext DatabaseContext { get; }

    private IPasswordHasher PasswordHasher { get; }

    public LoginController(IDatabaseContext databaseContext, IPasswordHasher passwordHasher)
    {
        this.DatabaseContext = databaseContext;
        this.PasswordHasher = passwordHasher;
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
    public async Task<IActionResult> Index(LoginModel form)
    {
        if (string.IsNullOrWhiteSpace(form.Username))
        {
            this.HttpContext.SetTempCookie("LoginErrorMessage", "Benutzername darf nicht leer sein.");

            return this.RedirectToAction("Index", "Login", new {redirect = form.Redirect});
        }

        if (string.IsNullOrWhiteSpace(form.Password))
        {
            this.HttpContext.SetTempCookie("LoginErrorMessage", "Passwort darf nicht leer sein.");

            return this.RedirectToAction("Index", "Login", new {redirect = form.Redirect});
        }

        var user = await this.DatabaseContext.FindUserById(form.Username);

        if (user == null)
        {
            this.HttpContext.SetTempCookie("LoginErrorMessage", "Es existiert kein Benutzer mit diesem Benutzernamen.");

            return this.RedirectToAction("Index", "Login", new {redirect = form.Redirect});
        }

        if (user.IsLocked)
        {
            this.HttpContext.SetTempCookie("LoginErrorMessage", "Dieser Benutzer ist gesperrt. Bitte kontaktieren Sie den Administrator.");

            return this.RedirectToAction("Index", "Login", new {redirect = form.Redirect});
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Id),
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

            //IsPersistent = true,
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

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        if (!string.IsNullOrWhiteSpace(form.Redirect) && this.Url.IsLocalUrl(form.Redirect))
        {
            return this.Redirect(form.Redirect);
        }

        return this.RedirectToAction("Index", "Home", new { Area= "Leiter" });
    }
}