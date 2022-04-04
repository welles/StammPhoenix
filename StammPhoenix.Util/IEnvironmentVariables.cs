namespace StammPhoenix.Util;

public interface IEnvironmentVariables
{
    void Validate();

    public string DatabaseHost { get; }

    public string DatabasePort { get; }

    public string DatabaseName { get; }

    public string DatabaseUsername { get; }

    public string DatabasePassword { get; }

    public string AdminPassword { get; }

    public string DataProtectionPath { get; }

    public string DownloadFilesPath { get; }
}
