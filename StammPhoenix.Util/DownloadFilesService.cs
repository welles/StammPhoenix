using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.WebUtilities;

namespace StammPhoenix.Util;

public class DownloadFilesService : IDownloadFilesService
{
    private IEnvironmentVariables EnvironmentVariables { get; }

    private IDataProtector DataProtector { get; }

    public DownloadFilesService(IEnvironmentVariables environmentVariables, IDataProtectionProvider dataProtectionProvider)
    {
        this.EnvironmentVariables = environmentVariables;
        this.DataProtector = dataProtectionProvider.CreateProtector(nameof(DownloadFilesService));
    }

    public IEnumerable<DownloadFile> GetFiles()
    {
        var files = Directory.GetFiles(this.EnvironmentVariables.DownloadFilesPath);

        foreach (var file in files)
        {
            var bytes = Encoding.UTF8.GetBytes(file);
            var protectedBytes = this.DataProtector.Protect(bytes);
            var urlFriendlyString = WebEncoders.Base64UrlEncode(protectedBytes);

            yield return new DownloadFile {DownloadKey = urlFriendlyString, FilePath = file};
        }
    }

    public DownloadFile? GetFile(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return null;
        }

        try
        {
            var protectedBytes = WebEncoders.Base64UrlDecode(key);
            var bytes = this.DataProtector.Unprotect(protectedBytes);
            var file = Encoding.UTF8.GetString(bytes);

            return new DownloadFile
            {
                DownloadKey = key,
                FilePath = file
            };
        }
        catch
        {
            return null;
        }
    }
}
