using Microsoft.EntityFrameworkCore;
using StammPhoenix.Persistence.Models;

namespace StammPhoenix.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DbSet<LoginUser> LoginUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = new DatabaseConnection();

            optionsBuilder.UseNpgsql(connection.ToString());

            base.OnConfiguring(optionsBuilder);
        }
    }
}