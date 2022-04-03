using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StammPhoenix.Persistence.Models;

namespace StammPhoenix.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext, IDesignTimeDbContextFactory<DatabaseContext>
    {
        private IDatabaseConnection DatabaseConnection { get; }

        private IHttpContextAccessor HttpContextAccessor { get; }

        private string? ConnectionStringOverride { get; }

        private DbSet<LoginUser> LoginUsers { get; set; }

        private DbSet<PlannedEvent> PlannedEvents { get; set; }

        private DbSet<PageContact> PageContacts { get; set; }

        public DatabaseContext(IDatabaseConnection databaseConnection, IHttpContextAccessor httpContextAccessor)
        {
            this.DatabaseConnection = databaseConnection;
            this.HttpContextAccessor = httpContextAccessor;

            this.SavingChanges += this.OnSavingChanges;
        }

        private void OnSavingChanges(object? sender, SavingChangesEventArgs e)
        {
            var entities = this.ChangeTracker.Entries().Where(x => x.Entity is Entity && x.State is EntityState.Added or EntityState.Modified);

            var username = this.HttpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? "SERVER";

            foreach (var entry in entities)
            {
                var entity = (Entity) entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                    entity.CreatedBy = username;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                    entity.ModifiedBy = username;
                }
            }
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LoginUser>()
                .HasIndex(x => x.Email)
                .IsUnique();
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
            var guid = Guid.Parse(id);

            return await this.LoginUsers.FindAsync(guid);
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

            var guid = Guid.Parse(id);

            var user = await this.LoginUsers.FindAsync(guid);

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
            var guid = Guid.Parse(id);

            var user = await this.LoginUsers.FindAsync(guid);

            if (user == null)
            {
                throw new InvalidOperationException($"User with ID \"{id}\" does not exist!");
            }

            user.NeedPasswordChange = needsPasswordChange;

            this.LoginUsers.Update(user);

            await this.SaveChangesAsync();
        }

        public async Task<Guid> CreateUser(LoginUser user)
        {
            await this.LoginUsers.AddAsync(user);

            await this.SaveChangesAsync();

            return user.Id;
        }

        public async Task Migrate()
        {
            await this.Database.MigrateAsync();
        }
    }
}
