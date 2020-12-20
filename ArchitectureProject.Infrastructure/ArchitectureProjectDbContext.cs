using System;
using System.ComponentModel.DataAnnotations.Schema;
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

            this.Connection = this.Database.IsSqlServer() ? this.Database.GetDbConnection() : null;

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

            modelBuilder.Entity<Role>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .Property(x => x.RoleId)
                .ValueGeneratedNever();

            modelBuilder.Entity<Role>().HasData(new Role[]
            {
                new Role()
                {
                    AddedDate = DateTime.Now,
                    Name = "Administrator",
                    RoleId = ArchitectureProject.Domain.Static.Roles.Administrator,
                },
                new Role()
                {
                    AddedDate = DateTime.Now,
                    Name = "Normal user",
                    RoleId = ArchitectureProject.Domain.Static.Roles.NormalUser,
                }
            });
        }
    }
}
