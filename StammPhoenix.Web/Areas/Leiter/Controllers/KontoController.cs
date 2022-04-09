﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StammPhoenix.Persistence;
using StammPhoenix.Util.Interfaces;
using StammPhoenix.Util.Models;
using StammPhoenix.Web.Areas.Leiter.Models.Konto;
using StammPhoenix.Web.Core;
using StammPhoenix.Web.Extensions;

namespace StammPhoenix.Web.Areas.Leiter.Controllers;

[Authorize]
[Area("Leiter")]
public class KontoController : Controller
{
    private IDatabaseContext DatabaseContext { get; }

    private ITempCookieService TempCookieService { get; }

    private IPasswordHasher PasswordHasher { get; }

    public KontoController(IDatabaseContext databaseContext, ITempCookieService tempCookieService, IPasswordHasher passwordHasher)
    {
        this.DatabaseContext = databaseContext;
        this.TempCookieService = tempCookieService;
        this.PasswordHasher = passwordHasher;
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
            this.TempCookieService.SetTempCookie("ChangePasswordErrorMessage", "Das alte Passwort muss angegeben werden.");

            return this.RedirectTo("ChangePassword", "Konto", "Leiter", form.Redirect);
        }

        if (string.IsNullOrWhiteSpace(form.NewPassword))
        {
            this.TempCookieService.SetTempCookie("ChangePasswordErrorMessage", "Das neue Passwort muss angegeben werden.");

            return this.RedirectTo("ChangePassword", "Konto", "Leiter", form.Redirect);
        }

        if (string.IsNullOrWhiteSpace(form.NewPasswordRepeat) || !form.NewPasswordRepeat.Equals(form.NewPassword))
        {
            this.TempCookieService.SetTempCookie("ChangePasswordErrorMessage", "Das Werte für das neue Passwort müssen übereinstimmen.");

            return this.RedirectTo("ChangePassword", "Konto", "Leiter", form.Redirect);
        }

        if (form.NewPassword.Equals(form.OldPassword))
        {
            this.TempCookieService.SetTempCookie("ChangePasswordErrorMessage", "Das neue Passwort muss sich vom alten unterscheiden.");

            return this.RedirectTo("ChangePassword", "Konto", "Leiter", form.Redirect);
        }

        var userId = this.HttpContext.GetUserId();

        var user = await this.DatabaseContext.FindUserById(userId);

        if (user == null)
        {
            this.TempCookieService.SetTempCookie("ChangePasswordErrorMessage", "Es existiert kein Benutzer mit diesem Benutzernamen.");

            return this.RedirectTo("ChangePassword", "Konto", "Leiter", form.Redirect);
        }

        var passwordCorrect = this.PasswordHasher.VerifyHashedPassword(user.PasswordHash, form.OldPassword);

        if (passwordCorrect == PasswordVerificationResult.Failed)
        {
            this.TempCookieService.SetTempCookie("ChangePasswordErrorMessage", "Das alte Passwort ist nicht korrekt.");

            return this.RedirectTo("ChangePassword", "Konto", "Leiter", form.Redirect);
        }

        var newPasswordHash = this.PasswordHasher.HashPassword(form.NewPassword!);

        await this.DatabaseContext.ChangeUserPassword(userId, newPasswordHash);

        await this.DatabaseContext.ChangeUserNeedsPasswordChange(userId, false);

        await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        this.TempCookieService.SetTempCookie("LoginInfoMessage", "Das Passwort wurde erfolgreich geändert. Bitte mit dem neuen Passwort neu anmelden.");

        return this.RedirectTo("Index", "Login", redirect: form.Redirect);
    }
}
