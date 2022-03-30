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
        sb.Append(version.Major);
        if (version.Minor != 0) { sb.AppendFormat(".{0}", version.Minor); }
        if (version.Build != 0) { sb.AppendFormat(".{0}", version.Build); }
        if (version.Revision != 0) { sb.AppendFormat(".{0}", version.Revision); }

        return sb.ToString();
    }

    public static DateTime GetBuildDate(this Assembly assembly)
    {
        const string BuildVersionMetadataPrefix = "+build";

        var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        if (attribute?.InformationalVersion != null)
        {
            var value = attribute.InformationalVersion;
            var index = value.IndexOf(BuildVersionMetadataPrefix, StringComparison.Ordinal);
            if (index > 0)
            {
                value = value.Substring(index + BuildVersionMetadataPrefix.Length);
                if (DateTime.TryParseExact(value, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var result))
                {
                    return result;
                }
            }
        }

        return default;
    }
}