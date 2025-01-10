using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.Data.Configurations;

public class CatalogConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(category => category.Id);

        builder.Property(category => category.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(category => category.Products)
            .WithOne(category => category.Category)
            .HasForeignKey(category => category.CategoryId)
            .IsRequired();
    }
}
