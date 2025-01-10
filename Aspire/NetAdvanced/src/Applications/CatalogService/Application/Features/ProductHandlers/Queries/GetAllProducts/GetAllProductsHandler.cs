using CatalogService.Application.Common.Interfaces.Database;
using CatalogService.Application.Common.Models;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Features.ProductHandlers.Queries.GetAllProducts;

public class GetAllProductsQuery : IRequest<ListResponse<Product>> { }

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, ListResponse<Product>>
{
    private readonly IReadOnlyApplicationDbContext _context;

    public GetAllProductsHandler(IReadOnlyApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ListResponse<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products.ToListAsync(cancellationToken);
        return new ListResponse<Product>(products);
    }
}
