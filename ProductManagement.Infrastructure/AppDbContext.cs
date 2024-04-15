using Microsoft.EntityFrameworkCore;
using ProductManagement.Infrastructure.EntityConfigurations;

namespace ProductManagement.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity mappings and relationships here
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            
        }
    }