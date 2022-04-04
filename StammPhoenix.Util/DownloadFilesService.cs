using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebUtilities;

namespace StammPhoenix.Util;

public class DownloadFilesService : IDownloadFilesService
{
    private IHostingEnvironment Environment { get; }

    private IEnvironmentVariables EnvironmentVariables { get; }

    private IDataProtector DataProtector { get; }

    public DownloadFilesService(IHostingEnvironment environment, IEnvironmentVariables environmentVariables, IDataProtectionProvider dataProtectionProvider)
    {
        this.Environment = environment;
        this.EnvironmentVariables = environmentVariables;
        this.DataProtector = dataProtectionProvider.CreateProtector(nameof(DownloadFilesService));
    }

    public IEnumerable<DownloadFile> GetDownloadFiles()
    {
        var files = Directory.GetFiles(this.EnvironmentVariables.DownloadFilesPath);

        foreach (var file in files)
        {
            var bytes = Encoding.UTF8.GetBytes(file);
            var protectedBytes = this.DataProtector.Protect(bytes);
            var urlFriendlyString = WebEncoders.Base64UrlEncode(protectedBytes);

            var name = Path.GetFileNameWithoutExtension(file);

            yield return new DownloadFile {Key = urlFriendlyString, Name = name};
        }
    }

    public string? GetFile(string key)
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

            return file;
        }
        catch
        {
            return null;
        }
    }
}
