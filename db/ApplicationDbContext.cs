using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiApiRestTest.Model;
using Microsoft.EntityFrameworkCore;

namespace MiApiRestTest.db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<ProductModel>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }
}