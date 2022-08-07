namespace StammPhoenix.Web.Models.Login;

public class LoginFormModel
{
    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool IsPersistent { get; set; }

    public string? Redirect { get; set; }
}
