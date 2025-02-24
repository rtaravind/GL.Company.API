using GL.Company.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace GL.Company.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TlCompany> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TlCompany>()
                .HasIndex(c => c.Isin)
                .IsUnique();
        }
    }
}
