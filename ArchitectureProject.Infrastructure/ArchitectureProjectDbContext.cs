using System.Data;
using ArchitectureProject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureProject.Infrastructure
{
    public sealed class ArchitectureProjectDbContext : DbContext
    {
        public IDbConnection Connection { get; set; }

        public ArchitectureProjectDbContext(DbContextOptions<ArchitectureProjectDbContext> options)
            : base(options)
        {
            this.Connection = this.Database.GetDbConnection();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
