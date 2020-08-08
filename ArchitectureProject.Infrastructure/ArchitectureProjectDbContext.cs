using System.Reflection;
using ArchitectureProject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureProject.Infrastructure
{
    public class ArchitectureProjectDbContext : DbContext
    {
        public ArchitectureProjectDbContext(DbContextOptions<ArchitectureProjectDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
