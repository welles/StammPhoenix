namespace StammPhoenix.Persistence
{
    public class DatabaseConnection
    {
        public const string HostParameter = "DATABASE_HOST";
        public const string PortParameter = "DATABASE_PORT";
        public const string DatabaseParameter = "DATABASE_NAME";
        public const string UsernameParameter = "DATABASE_USERNAME";
        public const string PasswordParameter = "DATABASE_PASSWORD";

        public DatabaseConnection()
        {
            if (File.Exists(@"..\StammPhoenix.Web\Dockerfile.env"))
            {
                var values = File.ReadAllLines(@"..\StammPhoenix.Web\Dockerfile.env")
                    .Select(x => x.Split("="))
                    .ToDictionary(x => x[0], x => x[1]);

                this.Host = values[HostParameter];
                this.Port = values[PortParameter];
                this.Database = values[DatabaseParameter];
                this.Username = values[UsernameParameter];
                this.Password = values[PasswordParameter];

                return;
            }

            this.Host = this.GetEnvironmentVariable(HostParameter);
            this.Port = this.GetEnvironmentVariable(PortParameter);
            this.Database = this.GetEnvironmentVariable(DatabaseParameter);
            this.Username = this.GetEnvironmentVariable(UsernameParameter);
            this.Password = this.GetEnvironmentVariable(PasswordParameter);
        }

        public string Host { get; }

        public string Port { get; }

        public string Database { get; }

        public string Username { get; }

        public string Password { get; }

        public override string ToString()
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
