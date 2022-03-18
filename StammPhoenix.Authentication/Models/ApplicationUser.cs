#pragma warning disable CS8618 // Disable nullable warning
using System.Security.Principal;

namespace StammPhoenix.Authentication.Models;

public class ApplicationUser : IIdentity
{
    public string AuthenticationType => "Forms";

    public bool IsAuthenticated { get; }

    public string Name { get; }

    public string Guid { get; }
}