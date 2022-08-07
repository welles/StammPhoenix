using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StammPhoenix.Persistence;
using StammPhoenix.Persistence.Constants;
using StammPhoenix.Persistence.Models;
using StammPhoenix.Web.Areas.Leiter.Models.Vorstand;
using StammPhoenix.Web.Core;
using StammPhoenix.Web.Extensions;

namespace StammPhoenix.Web.Areas.Leiter.Controllers;

[Authorize(Roles = nameof(Role.Administrator))]
[Area("Leiter")]
public class VorstandController : Controller
{
    private IDatabaseContext DatabaseContext { get; }

    private IMapper Mapper { get; }

    private ITempCookieService TempCookieService { get; }

    public VorstandController(IDatabaseContext databaseContext, IMapper mapper, ITempCookieService tempCookieService)
    {
        this.DatabaseContext = databaseContext;
        this.Mapper = mapper;
        this.TempCookieService = tempCookieService;
    }

    [HttpGet]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Index()
    {
        var contacts = await this.DatabaseContext.GetPageContacts();

        var viewModel = new VorstandViewModel
        {
            VorstandModels = contacts.Select(x => this.Mapper.Map<VorstandModel>(x)).ToList()
        };

        if (this.TempCookieService.TryGetTempCookie("VorstandErrorMessage", out var errorMessage))
        {
            viewModel.ErrorMessage = errorMessage;
        }

        if (this.TempCookieService.TryGetTempCookie("VorstandInfoMessage", out var infoMessage))
        {
            viewModel.InfoMessage = infoMessage;
        }

        return this.View(viewModel);
    }

    [HttpGet]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            this.TempCookieService.SetTempCookie("VorstandErrorMessage", "Es wurde keine ID angegeben.");
            return this.RedirectTo("Index", "Vorstand", "Leiter");
        }

        var contact = await this.DatabaseContext.FindPageContactById(id.Value);

        if (contact == null)
        {
            this.TempCookieService.SetTempCookie("VorstandErrorMessage", "Dieser Vorstand existiert nicht mehr.");
            return this.RedirectTo("Index", "Vorstand", "Leiter");
        }

        var viewModel = this.Mapper.Map<EditVorstandViewModel>(contact);

        if (this.TempCookieService.TryGetTempCookie("VorstandErrorMessage", out var errorMessage))
        {
            viewModel.ErrorMessage = errorMessage;
        }

        return this.View(viewModel);
    }

    [HttpPost]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Edit(EditVorstandFormModel? form)
    {
        if (form?.Id == null || string.IsNullOrWhiteSpace(form.Name) || string.IsNullOrWhiteSpace(form.PhoneNumber)
            || string.IsNullOrWhiteSpace(form.AddressStreet) || string.IsNullOrWhiteSpace(form.AddressCity))
        {
            var viewModel = this.Mapper.Map<EditVorstandViewModel>(form);
            viewModel.ErrorMessage = "Die Felder dürfen nicht leer sein.";

            return this.View(viewModel);
        }

        var contact = await this.DatabaseContext.FindPageContactById(form.Id.Value);

        if (contact == null)
        {
            this.TempCookieService.SetTempCookie("VorstandErrorMessage", "Dieser Vorstand existiert nicht mehr.");
            return this.RedirectTo("Index", "Vorstand", "Leiter");
        }

        await this.DatabaseContext.UpdatePageContact(contact, form.Name, form.PhoneNumber, form.AddressStreet, form.AddressCity);

        this.TempCookieService.SetTempCookie("VorstandInfoMessage", "Die Kontaktdaten wurden erfolgreich geändert.");

        return this.RedirectTo("Index", "Vorstand", "Leiter");
    }

    [HttpGet]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            this.TempCookieService.SetTempCookie("VorstandErrorMessage", "Es wurde keine ID angegeben.");
            return this.RedirectTo("Index", "Vorstand", "Leiter");
        }

        var contact = await this.DatabaseContext.FindPageContactById(id.Value);

        if (contact == null)
        {
            this.TempCookieService.SetTempCookie("VorstandErrorMessage", "Dieser Vorstand existiert nicht mehr.");
            return this.RedirectTo("Index", "Vorstand", "Leiter");
        }

        await this.DatabaseContext.RemovePageContact(contact);

        this.TempCookieService.SetTempCookie("VorstandInfoMessage", "Die Kontaktdaten wurden erfolgreich entfernt.");

        return this.RedirectTo("Index", "Vorstand", "Leiter");
    }

    [HttpGet]
    [Authorize(Roles = nameof(Role.Administrator))]
    public IActionResult Create()
    {
        var viewModel = new CreateVorstandViewModel();

        if (this.TempCookieService.TryGetTempCookie("VorstandErrorMessage", out var errorMessage))
        {
            viewModel.ErrorMessage = errorMessage;
        }

        return this.View(viewModel);
    }

    [HttpPost]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Create(CreateVorstandFormModel? form)
    {
        if (form == null || string.IsNullOrWhiteSpace(form.Name) || string.IsNullOrWhiteSpace(form.PhoneNumber) ||
            string.IsNullOrWhiteSpace(form.AddressStreet) || string.IsNullOrWhiteSpace(form.AddressCity))
        {
            var viewModel = this.Mapper.Map<CreateVorstandViewModel>(form);
            viewModel.ErrorMessage = "Die Felder dürfen nicht leer sein.";

            return this.View(viewModel);
        }

        await this.DatabaseContext.CreatePageContact(form.Name, form.PhoneNumber, form.AddressStreet, form.AddressCity);

        this.TempCookieService.SetTempCookie("VorstandInfoMessage", "Die Kontaktdaten wurden erfolgreich angelegt.");

        return this.RedirectTo("Index", "Vorstand", "Leiter");
    }
}
