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
    }
}
