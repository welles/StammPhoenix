using StammPhoenix.Util.Models;

namespace StammPhoenix.Util.Interfaces;

public interface IDownloadFilesService
{
    IEnumerable<DownloadFile> GetFiles();

    DownloadFile? GetFile(string key);
}
