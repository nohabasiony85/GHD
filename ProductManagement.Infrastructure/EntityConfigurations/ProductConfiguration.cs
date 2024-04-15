
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagement.Domain;

namespace ProductManagement.Infrastructure.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Brand)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Price)
            .IsRequired();
        
        //Seed
        builder.HasData(new List<Product>
        {
            new Product { Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), Name = "Product 1", Brand = "Brand A", Price = 10.99m },
            new Product { Id = Guid.NewGuid(), Name = "Product 2", Brand = "Brand B", Price = 20.49m },
            new Product { Id = Guid.NewGuid(), Name = "Product 3", Brand = "Brand C", Price = 15.79m }
            // Add more products as needed
        });

        // Additional configurations can be added here
    }
}
