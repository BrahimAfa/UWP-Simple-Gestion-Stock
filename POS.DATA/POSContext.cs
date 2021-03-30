using Microsoft.EntityFrameworkCore;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Data
{
  public class POSContext : DbContext
  {
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Provider> Providers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite("Filename=POSDB1.db");
      optionsBuilder.EnableSensitiveDataLogging(true);
      optionsBuilder.EnableDetailedErrors(true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Make Product.Name required
      modelBuilder.Entity<Product>()
          .Property(b => b.Name)
          .IsRequired();
    }
  }
}
