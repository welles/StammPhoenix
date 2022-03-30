﻿#pragma warning disable CS8618 // Disable nullable warning
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StammPhoenix.Persistence.Constants;
using StammPhoenix.Persistence.Models;

namespace StammPhoenix.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext, IDesignTimeDbContextFactory<DatabaseContext>
    {
        private IDatabaseConnection DatabaseConnection { get; }

        private string? ConnectionStringOverride { get; }

        private DbSet<LoginUser> LoginUsers { get; set; }

        private DbSet<PlannedEvent> PlannedEvents { get; set; }

        private DbSet<PageContact> PageContacts { get; set; }

        private DbSet<Setting> Settings { get; set; }

        public DatabaseContext(IDatabaseConnection databaseConnection)
        {
            this.DatabaseConnection = databaseConnection;
        }

        public DatabaseContext(string connectionString)
        {
            this.ConnectionStringOverride = connectionString;
        }

        public DatabaseContext() { }

        public DatabaseContext CreateDbContext(string[] args)
        {
            return new DatabaseContext(string.Join(" ", args));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>()
                .HasData(new Setting {Id = SettingNames.InitialSetupComplete, Value = "false"});
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(this.ConnectionStringOverride ?? this.DatabaseConnection.GetConnectionString());

            base.OnConfiguring(optionsBuilder);
        }

        public async Task<PlannedEvent[]> GetPlannedEvents()
        {
            return await this.PlannedEvents.ToArrayAsync();
        }

        public async Task<LoginUser?> FindUserById(string id)
        {
            return await this.LoginUsers.FindAsync(id);
        }

        public async Task<LoginUser?> FindUserByEmail(string email)
        {
            return await this.LoginUsers.SingleOrDefaultAsync(x => x.Email.ToUpper().Equals(email.ToUpper()));
        }

        public async Task ChangeUserPassword(string id, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException(nameof(passwordHash));
            }

            var user = await this.LoginUsers.FindAsync(id);

            if (user == null)
            {
                throw new InvalidOperationException($"User with ID \"{id}\" does not exist!");
            }

            user.PasswordHash = passwordHash;

            this.LoginUsers.Update(user);

            await this.SaveChangesAsync();
        }

        public async Task ChangeUserNeedsPasswordChange(string id, bool needsPasswordChange)
        {
            var user = await this.LoginUsers.FindAsync(id);

            if (user == null)
            {
                throw new InvalidOperationException($"User with ID \"{id}\" does not exist!");
            }

            user.NeedPasswordChange = needsPasswordChange;

            this.LoginUsers.Update(user);

            await this.SaveChangesAsync();
        }

        public async Task CreateUser(LoginUser user)
        {
            await this.LoginUsers.AddAsync(user);

            await this.SaveChangesAsync();
        }

        public async Task UpdateSetting(string name, object value)
        {
            var valueString = value.ToString();

            if (valueString == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var setting = await this.Settings.FindAsync(name);

            if (setting == null)
            {
                throw new InvalidOperationException($"Setting with name \"{name}\" does not exist!");
            }

            setting.Value = valueString;

            this.Settings.Update(setting);

            await this.SaveChangesAsync();
        }

        public async Task<T?> GetSetting<T>(string name)
        {
            var setting = await this.Settings.FindAsync(name);

            if (setting == null)
            {
                return default;
            }

            var value = TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(setting.Value);

            if (value == null)
            {
                return default;
            }

            return (T) value;
        }

        public async Task Migrate()
        {
            await this.Database.MigrateAsync();
        }
    }
}