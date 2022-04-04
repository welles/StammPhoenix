namespace StammPhoenix.Util;

public interface IDownloadFilesService
{
    IEnumerable<DownloadFile> GetDownloadFiles();

    string? GetFile(string key);
}
