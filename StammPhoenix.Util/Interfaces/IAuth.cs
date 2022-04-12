namespace StammPhoenix.Util.Interfaces;

public interface IAuth
{
    bool IsAuthenticated();

    string? GetUserId();

    string? GetUserEmail();

    string? GetUserGivenName();

    bool GetUserNeedsPasswordChange();
}