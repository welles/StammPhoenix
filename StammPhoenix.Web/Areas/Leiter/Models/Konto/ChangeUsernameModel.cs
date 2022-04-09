namespace StammPhoenix.Web.Areas.Leiter.Models.Konto;

public class ChangeUsernameModel
{
    public string? Redirect { get; set; }

    public string? CurrentUsername { get; set; }

    public string? NewUsername { get; set; }
}