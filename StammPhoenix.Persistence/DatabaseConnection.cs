namespace StammPhoenix.Persistence
{
    public class DatabaseConnection
    {
        public DatabaseConnection()
        {
            Host = Environment.GetEnvironmentVariable("DATABASE_HOST");
            Port = int.Parse(Environment.GetEnvironmentVariable("DATABASE_PORT"));
            Database = Environment.GetEnvironmentVariable("DATABASE_NAME");
            Username = Environment.GetEnvironmentVariable("DATABASE_USERNAME");
            Password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
        }

        public string Host { get; }

        public int Port { get; }

        public string Database { get; }

        public string Username { get; }

        public string Password { get; }

        public override string? ToString()
        {
            return $"Host={Host}:{Port};Database={Database};Username={Username};Password={Password}";
        }
    }
}
