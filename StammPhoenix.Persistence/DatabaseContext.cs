using Microsoft.EntityFrameworkCore;
using StammPhoenix.Persistence.Models;

namespace StammPhoenix.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<LoginUser> LoginUsers { get; set; }

        public DbSet<PlannedEvent> PlannedEvents { get; set; }

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
    }
}