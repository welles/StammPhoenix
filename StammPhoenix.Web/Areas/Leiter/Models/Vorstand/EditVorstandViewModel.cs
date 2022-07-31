namespace StammPhoenix.Web.Areas.Leiter.Models.Vorstand;

public class EditVorstandViewModel
{
    public string? ErrorMessage { get; set; }

    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? AddressCity { get; set; }

    public string? AddressStreet { get; set; }
}
