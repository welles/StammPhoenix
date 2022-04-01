using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;

namespace StammPhoenix.Web.Core;

public class TempCookieService : ITempCookieService
{
    private const string CookiePrefix = "TEMP-";

    private static readonly Regex CookieMatchRegex = new(@"TEMP-[0-9A-Z]{8}(?:-[0-9A-Z]{4}){3}-[0-9A-Z]{12}");

    private IDataProtector DataProtector { get; }

    public TempCookieService(IDataProtectionProvider dataProtectionProvider)
    {
        this.DataProtector = dataProtectionProvider.CreateProtector(nameof(TempCookieService));
    }

    public void SetTempCookie(HttpContext context, string key, string value)
    {
        var data = this.GetCookies(context);

        var existingValue = data.Where(x => x.Value.DataKey.Equals(key)).ToArray();

        if (existingValue.Any())
        {
            existingValue.Single().Value.DataValue = value;
            existingValue.Single().Value.TimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        }
        else
        {
            var newCookie = new CookieValue
            {
                TimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
                DataKey = key,
                DataValue = value,
                Remove = false
            };

            data.Add(TempCookieService.CookiePrefix + Guid.NewGuid().ToString().ToUpper(), newCookie);
        }

        this.UpdateCookies(context, data);
    }

    public bool TryGetTempCookie(HttpContext context, string key, out string? value)
    {
        var data = this.GetCookies(context);

        KeyValuePair<string, CookieValue>? existingValue;

        var cookies = data.Where(x => x.Value.DataKey.Equals(key)).ToArray();

        switch (cookies.Length)
        {
            case 1:
                existingValue = cookies.Single();
                break;
            case > 1:
                {
                    existingValue = cookies.OrderByDescending(x => x.Value.TimeStamp).First();
                    foreach (var oldCookie in cookies.Except(new[] { existingValue.Value }))
                    {
                        oldCookie.Value.Remove = true;
                    }
                    break;
                }
            default:
                value = null;
                return false;
        }

        value = existingValue.Value.Value.DataValue;
        existingValue.Value.Value.Remove = true;

        this.UpdateCookies(context, data);

        return true;
    }

    #region Helpers

    private void UpdateCookies(HttpContext context, IDictionary<string, CookieValue> cookies)
    {
        foreach (var (name, value) in cookies)
        {
            if (value.Remove)
            {
                context.Response.Cookies.Delete(name);
            }
            else
            {
                var serializedData = JsonConvert.SerializeObject(value);

                var unprotectedBytes = Encoding.UTF8.GetBytes(serializedData);

                var protectedBytes = this.DataProtector.Protect(unprotectedBytes);

                var protectedString = Convert.ToBase64String(protectedBytes);

                context.Response.Cookies.Append(name, protectedString);
            }
        }
    }

    private Dictionary<string, CookieValue> GetCookies(HttpContext context)
    {
        var cookies = context.Request.Cookies.Where(x => TempCookieService.CookieMatchRegex.IsMatch(x.Key)).ToArray();

        if (cookies.Length == 0)
        {
            return new Dictionary<string, CookieValue>();
        }

        var values = new Dictionary<string, CookieValue>();

        foreach (var cookie in cookies)
        {
            var protectedBytes = Convert.FromBase64String(cookie.Value);

            var unprotectedBytes = this.DataProtector.Unprotect(protectedBytes);

            var unprotectedString = Encoding.UTF8.GetString(unprotectedBytes);

            var cookieValue = JsonConvert.DeserializeObject<CookieValue>(unprotectedString) ?? new CookieValue
            {
                Remove = true,
                DataKey = Guid.NewGuid().ToString(),
                DataValue = string.Empty,
                TimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds()
            };

            values.Add(cookie.Key, cookieValue);
        }

        return values;
    }

    [JsonObject]
    private class CookieValue
    {
        public string DataKey { get; set; }

        public string DataValue { get; set; }

        public long TimeStamp { get; set; }

        public bool Remove { get; set; }
    }

    #endregion Helpers
}
