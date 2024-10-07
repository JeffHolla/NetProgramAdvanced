using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Data;

public class ReadOnlyApplicationDbContext : IReadOnlyApplicationDbContext
{
    private readonly IApplicationDbContext _context;

    public ReadOnlyApplicationDbContext(IApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Product> Products => _context.Products.AsNoTracking();
    public IQueryable<Category> Categories => _context.Categories.AsNoTracking();
}
