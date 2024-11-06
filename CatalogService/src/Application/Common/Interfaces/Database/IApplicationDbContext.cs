using CatalogService.Domain.Entities;

namespace CatalogService.Application.Common.Interfaces.Database;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Category> Categories { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
