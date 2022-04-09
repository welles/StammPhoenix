using StammPhoenix.Persistence.Constants;
using StammPhoenix.Persistence.Models;
using StammPhoenix.Util.Interfaces;

namespace StammPhoenix.Persistence;

public class DatabaseValidator : IDatabaseValidator
{
    private const string AdminEmail = "admin@dpsg-feldkirchen.de";
    private const string AdminName = "Administrator";

    private IDatabaseContext DatabaseContext { get; }

    private IPasswordHasher PasswordHasher { get; }

    private IEnvironmentVariables EnvironmentVariables { get; }

    public DatabaseValidator(IDatabaseContext databaseContext, IPasswordHasher passwordHasher, IEnvironmentVariables environmentVariables)
    {
        this.DatabaseContext = databaseContext;
        this.PasswordHasher = passwordHasher;
        this.EnvironmentVariables = environmentVariables;
    }

    public async Task Validate()
    {
        await this.DatabaseContext.Migrate();

        var admin = await this.DatabaseContext.FindUserByEmail(DatabaseValidator.AdminEmail);

        if (admin == null)
        {
            var adminPasswordHash = this.PasswordHasher.HashPassword(this.EnvironmentVariables.AdminPassword);

            admin = new LoginUser
            {
                Email = DatabaseValidator.AdminEmail,
                Name = DatabaseValidator.AdminName,
                Role = Role.Administrator,
                IsLocked = false,
                NeedPasswordChange = false,
                PasswordHash = adminPasswordHash
            };

            await this.DatabaseContext.CreateUser(admin);
        }
    }
}
