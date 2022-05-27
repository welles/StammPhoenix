using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StammPhoenix.Persistence.Constants;
using StammPhoenix.Persistence.Models;
using StammPhoenix.Util.Interfaces;

namespace StammPhoenix.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext, IDesignTimeDbContextFactory<DatabaseContext>
    {
        private IDatabaseConnection DatabaseConnection { get; }

        private IHttpContextAccessor HttpContextAccessor { get; }

        private IAuth Auth { get; }

        private string? ConnectionStringOverride { get; }

        private DbSet<LoginUser> LoginUsers { get; set; }

        private DbSet<PlannedEvent> PlannedEvents { get; set; }

        private DbSet<PageContact> PageContacts { get; set; }

        private DbSet<Team> Teams { get; set; }

        public DatabaseContext(IDatabaseConnection databaseConnection, IHttpContextAccessor httpContextAccessor, IAuth auth)
        {
            this.DatabaseConnection = databaseConnection;
            this.HttpContextAccessor = httpContextAccessor;
            this.Auth = auth;

            this.SavingChanges += this.OnSavingChanges;
        }

        private void OnSavingChanges(object? sender, SavingChangesEventArgs e)
        {
            var entities = this.ChangeTracker.Entries().Where(x => x.Entity is Entity && x.State is EntityState.Added or EntityState.Modified);

            var username = this.Auth.GetUserEmail() ?? "SERVER";

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

            modelBuilder.Entity<Team>()
                .HasIndex(x => x.Rank)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(this.ConnectionStringOverride ?? this.DatabaseConnection.GetConnectionString());

            base.OnConfiguring(optionsBuilder);
        }

        public async Task<Team?> FindTeamForRank(Rank rank)
        {
            return await this.Teams.FindAsync(rank);
        }

        public async Task<bool> VerifyConnection()
        {
            return await this.Database.CanConnectAsync();
        }

        public async Task<PlannedEvent[]> GetPlannedEvents()
        {
            return await this.PlannedEvents.ToArrayAsync();
        }

        public async Task<PageContact[]> GetPageContacts()
        {
            return await this.PageContacts.ToArrayAsync();
        }

        public async Task<Team[]> GetTeams()
        {
            return await this.Teams.ToArrayAsync();
        }

        public async Task<LoginUser?> FindUserById(Guid id)
        {
            return await this.LoginUsers.FindAsync(id);
        }

        public async Task<LoginUser?> FindUserByEmail(string email)
        {
            return await this.LoginUsers.SingleOrDefaultAsync(x => x.Email.ToUpper().Equals(email.ToUpper()));
        }

        public async Task ChangeUserPassword(LoginUser user, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException(nameof(passwordHash));
            }

            user.PasswordHash = passwordHash;

            this.LoginUsers.Update(user);

            await this.SaveChangesAsync();
        }

        public async Task ChangeUserNeedsPasswordChange(LoginUser user, bool needsPasswordChange)
        {
            user.NeedPasswordChange = needsPasswordChange;

            this.LoginUsers.Update(user);

            await this.SaveChangesAsync();
        }

        public async Task ChangeUserEmail(LoginUser user, string newEmail)
        {
            user.Email = newEmail;

            this.LoginUsers.Update(user);

            await this.SaveChangesAsync();
        }

        public async Task ChangeUserSecurityStamp(LoginUser user, Guid newSecurityStamp)
        {
            user.SecurityStamp = newSecurityStamp;

            this.LoginUsers.Update(user);

            await this.SaveChangesAsync();
        }

        public async Task<bool> VerifyUserSecurityStamp(Guid userId, Guid securityStamp)
        {
            var user = await this.LoginUsers.FindAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("Diese Nutzer existiert nicht!");
            }

            return user.SecurityStamp == null || securityStamp.Equals(user.SecurityStamp);
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
