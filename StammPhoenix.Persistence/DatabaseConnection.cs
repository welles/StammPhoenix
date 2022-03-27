namespace StammPhoenix.Persistence
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public DatabaseConnection()
        {
            this.Host = this.GetEnvironmentVariable("DATABASE_HOST");
            this.Port = this.GetEnvironmentVariable("DATABASE_PORT");
            this.Database = this.GetEnvironmentVariable("DATABASE_NAME");
            this.Username = this.GetEnvironmentVariable("DATABASE_USERNAME");
            this.Password = this.GetEnvironmentVariable("DATABASE_PASSWORD");
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

        private string GetEnvironmentVariable(string name)
        {
            var value = Environment.GetEnvironmentVariable(name);

            if (value == null)
            {
                throw new InvalidOperationException($"Environment variable {name} is not set");
            }

            return value;
        }
    }
}
