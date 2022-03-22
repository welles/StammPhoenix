#pragma warning disable CS8618 // Disable nullable warning
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using StammPhoenix.Persistence.Constants;
using StammPhoenix.Persistence.Models;

namespace StammPhoenix.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        private DbSet<LoginUser> LoginUsers { get; set; }

        private DbSet<PlannedEvent> PlannedEvents { get; set; }

        private DbSet<PageContact> PageContacts { get; set; }

        private DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>()
                .HasData(new Setting {Id = SettingNames.InitialSetupComplete, Value = "false"});
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = new DatabaseConnection();

            optionsBuilder.UseNpgsql(connection.ToString());

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
    }
}