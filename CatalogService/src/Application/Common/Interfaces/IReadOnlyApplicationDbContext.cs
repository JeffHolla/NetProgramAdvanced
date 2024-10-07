using CatalogService.Domain.Entities;

namespace CatalogService.Application.Common.Interfaces;

public interface IReadOnlyApplicationDbContext
{
    IQueryable<Product> Products { get; }
    IQueryable<Category> Categories { get; }
}
