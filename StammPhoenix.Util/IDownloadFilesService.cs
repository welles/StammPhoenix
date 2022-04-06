using System.Collections.Generic;

namespace StammPhoenix.Util;

public interface IDownloadFilesService
{
    IEnumerable<DownloadFile> GetFiles();

    DownloadFile? GetFile(string key);
}
