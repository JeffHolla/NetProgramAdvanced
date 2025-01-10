using CatalogService.Domain.Entities;

namespace CatalogService.Application.Common.Interfaces.Database;

public interface IReadOnlyApplicationDbContext
{
    IQueryable<Product> Products { get; }
    IQueryable<Category> Categories { get; }
}
