namespace StammPhoenix.Persistence
{
    public class DatabaseConnection
    {
        public DatabaseConnection()
        {
            this.Host = Environment.GetEnvironmentVariable("DATABASE_HOST");
            this.Port = int.Parse(Environment.GetEnvironmentVariable("DATABASE_PORT"));
            this.Database = Environment.GetEnvironmentVariable("DATABASE_NAME");
            this.Username = Environment.GetEnvironmentVariable("DATABASE_USERNAME");
            this.Password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
        }

        public string Host { get; }

        public int Port { get; }

        public string Database { get; }

        public string Username { get; }

        public string Password { get; }

        public override string? ToString()
        {
            return $"Host={this.Host}:{this.Port};Database={this.Database};Username={this.Username};Password={this.Password}";
        }
    }
}
