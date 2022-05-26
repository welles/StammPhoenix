namespace StammPhoenix.Util.Interfaces;

public interface IAuth
{
    bool IsAuthenticated();

    Guid? GetUserId();

    string? GetUserEmail();

    string? GetUserGivenName();

    Guid? GetUserSecurityStamp();

    bool GetUserNeedsPasswordChange();
}
