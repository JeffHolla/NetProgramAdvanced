using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(product => product.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(product => product.Category)
            .IsRequired();

        builder.Property(product => product.Price)
            .IsRequired();

        builder.Property(product => product.Amount)
            .IsRequired();

        // TODO: Check this one more time and in DB
        // "one item can belong to only one category"
        builder.HasOne(entity => entity.Category).WithOne();
    }
}
