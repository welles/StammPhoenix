﻿namespace StammPhoenix.Web.Areas.Leiter.Models.Konto;

public class ChangePasswordModel
{
    public string? OldPassword { get; set; }

    public string? NewPassword { get; set; }

    public string? NewPasswordRepeat { get; set; }

    public string? Redirect { get; set; }
}
