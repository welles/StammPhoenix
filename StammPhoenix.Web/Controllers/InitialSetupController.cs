using Microsoft.AspNetCore.Mvc;
using StammPhoenix.Persistence;
using StammPhoenix.Persistence.Constants;
using StammPhoenix.Persistence.Models;
using StammPhoenix.Web.Core;
using StammPhoenix.Web.Models.InitialSetup;

namespace StammPhoenix.Web.Controllers;

public class InitialSetupController : Controller
{
    private IDatabaseContext DatabaseContext { get; }

    private IPasswordHasher PasswordHasher { get; }

    public InitialSetupController(IDatabaseContext databaseContext, IPasswordHasher passwordHasher)
    {
        this.DatabaseContext = databaseContext;
        this.PasswordHasher = passwordHasher;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(InitialSetupModel form)
    {
        if (string.IsNullOrWhiteSpace(form.Username)
            || string.IsNullOrWhiteSpace(form.DisplayName)
            || string.IsNullOrWhiteSpace(form.Password)
            || string.IsNullOrWhiteSpace(form.RepeatPassword))
        {
            throw new NotImplementedException();
        }

        var hashedPassword = this.PasswordHasher.HashPassword(form.Password);

        var user = new LoginUser
        {
            Id = form.Username,
            Name = form.DisplayName,
            PasswordHash = hashedPassword,
            IsLocked = false,
            NeedPasswordChange = false
        };

        await this.DatabaseContext.CreateUser(user);

        await this.DatabaseContext.UpdateSetting(SettingNames.InitialSetupComplete, true);

        return this.RedirectToAction("Index", "Login");
    }
}