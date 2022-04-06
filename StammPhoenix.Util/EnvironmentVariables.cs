using System;
using System.Collections.Generic;
using System.Linq;

namespace StammPhoenix.Util;

public class EnvironmentVariables: IEnvironmentVariables
{
    private readonly List<string> MissingVariables = new List<string>();

    public EnvironmentVariables()
    {
        this.DatabaseHost = this.GetValue("DATABASE_HOST")!;
        this.DatabasePort = this.GetValue("DATABASE_PORT")!;
        this.DatabaseName = this.GetValue("DATABASE_NAME")!;
        this.DatabaseUsername = this.GetValue("DATABASE_USERNAME")!;
        this.DatabasePassword = this.GetValue("DATABASE_PASSWORD")!;
        this.AdminPassword = this.GetValue("ADMIN_PASSWORD")!;
        this.DataProtectionPath = this.GetValue("ASPNETCORE_DataProtection__Path")!;
        this.DownloadFilesPath = this.GetValue("DOWNLOAD_FILES_PATH")!;
    }

    public void Validate()
    {
        if (this.MissingVariables.Any())
        {
            throw new InvalidOperationException($"The following environment variables are missing and must be set to continue: {string.Join(", ", this.MissingVariables)}");
        }
    }

    private string? GetValue(string name)
    {
        var value = Environment.GetEnvironmentVariable(name);

        if (string.IsNullOrWhiteSpace(value))
        {
            this.MissingVariables.Add(name);
        }

        return value;
    }

    public string DatabaseHost { get; }

    public string DatabasePort { get; }

    public string DatabaseName { get; }

    public string DatabaseUsername { get; }

    public string DatabasePassword { get; }

    public string AdminPassword { get; }

    public string DataProtectionPath { get; }

    public string DownloadFilesPath { get; }
}
