﻿using StammPhoenix.Persistence;
using StammPhoenix.Persistence.Constants;
using StammPhoenix.Persistence.Models;

namespace StammPhoenix.Web.Core;

public class DatabaseValidator : IDatabaseValidator
{
    private const string AdminEmail = "admin@dpsg-feldkirchen.de";
    private const string AdminName = "Administrator";

    private IDatabaseContext DatabaseContext { get; }

    private IPasswordHasher PasswordHasher { get; }

    public DatabaseValidator(IDatabaseContext databaseContext, IPasswordHasher passwordHasher)
    {
        this.DatabaseContext = databaseContext;
        this.PasswordHasher = passwordHasher;
    }

     public async Task Validate()
     {
         await this.DatabaseContext.Migrate();

         var admin = await this.DatabaseContext.FindUserByEmail(DatabaseValidator.AdminEmail);

         if (admin == null)
         {
             var adminPasswordHash = this.PasswordHasher.HashPassword(Environment.GetEnvironmentVariable("ADMIN_PASSWORD")!);

             admin = new LoginUser
             {
                 Id = Guid.NewGuid().ToString().ToUpper(),
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