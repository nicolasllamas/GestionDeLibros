using Data_Access.Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Access
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<LogError> LogErrors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;" +
                                        "Database=GestionDeLibrosDb;" +
                                        "Trusted_Connection=True;" +
                                        "Encrypt=False;");
        }
    }
}
