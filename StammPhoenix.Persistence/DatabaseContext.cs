#pragma warning disable CS8618 // Disable nullable warning
using Microsoft.EntityFrameworkCore;
using StammPhoenix.Persistence.Models;

namespace StammPhoenix.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        private DbSet<LoginUser> LoginUsers { get; set; }

        private DbSet<PlannedEvent> PlannedEvents { get; set; }

        private DbSet<PageContact> PageContacts { get; set; }

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