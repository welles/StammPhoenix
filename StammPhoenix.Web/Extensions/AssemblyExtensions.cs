using System.Globalization;
using System.Reflection;
using System.Text;

namespace StammPhoenix.Web.Extensions;

public static class AssemblyExtensions
{
    public static string GetVersion(this Assembly assembly)
    {
        var version = assembly?.GetName().Version ?? new Version(0, 0, 0, 0);

        var sb = new StringBuilder("v");
        sb.Append(version);

        return sb.ToString();
    }

    private const string BuildVersionMetadataPrefix = "+build";

    public static DateTime GetBuildDate(this Assembly assembly)
    {
        var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        if (attribute?.InformationalVersion != null)
        {
            var value = attribute.InformationalVersion;
            var index = value.IndexOf(BuildVersionMetadataPrefix, StringComparison.Ordinal);
            if (index > 0)
            {
                value = value.Substring(index + BuildVersionMetadataPrefix.Length);
                if (DateTime.TryParseExact(value, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                {
                    var utc = DateTime.SpecifyKind(result, DateTimeKind.Utc);

                    return TimeZoneInfo.ConvertTime(utc, TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"));
                }
            }
        }

        return default;
    }
}
