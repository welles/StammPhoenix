namespace StammPhoenix.Web.Core;

public interface ITempCookieService
{
    void SetTempCookie(HttpContext context, string key, string value);

    bool TryGetTempCookie(HttpContext context, string key, out string? value);
}
