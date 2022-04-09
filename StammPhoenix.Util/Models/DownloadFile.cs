namespace StammPhoenix.Util.Models;

public struct DownloadFile
{
    public string DownloadKey { get; init; }

    public string FilePath { get; init; }

    public string Name => Path.GetFileNameWithoutExtension(this.FilePath);

    public string Extension => Path.GetExtension(this.FilePath);

    public string NameWithExtension => Path.GetFileName(this.FilePath);
}
