using StammPhoenix.Util;

namespace StammPhoenix.Persistence
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public DatabaseConnection(IEnvironmentVariables environmentVariables)
        {
            this.Host = environmentVariables.DatabaseHost;
            this.Port = environmentVariables.DatabasePort;
            this.Database = environmentVariables.DatabaseName;
            this.Username = environmentVariables.DatabaseUsername;
            this.Password = environmentVariables.DatabasePassword;
        }

        private string Host { get; }

        private string Port { get; }

        private string Database { get; }

        private string Username { get; }

        private string Password { get; }

        public string GetConnectionString()
        {
            return $"Host={this.Host}:{this.Port};Database={this.Database};Username={this.Username};Password={this.Password}";
        }
    }
}
