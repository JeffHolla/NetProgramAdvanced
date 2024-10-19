using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);

        builder.Property(product => product.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(product => product.Price)
            .IsRequired();

        builder.Property(product => product.Amount)
            .IsRequired();
        builder.ToTable(builder => 
            builder.HasCheckConstraint("PositiveAmountConstraint", "Amount >= 0")
        );
    }
}
