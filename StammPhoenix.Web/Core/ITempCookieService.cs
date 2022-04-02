namespace StammPhoenix.Web.Core;

public interface ITempCookieService
{
    void SetTempCookie(string key, string value);

    bool TryGetTempCookie(string key, out string? value);
}
