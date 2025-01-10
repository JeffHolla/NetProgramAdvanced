using CatalogService.Application.Common.Interfaces.Database;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Features.ProductHandlers.Queries.GetProduct;

public class GetProductQuery : IRequest<Product>
{
    public int ProductId { get; set; }
}

public class GetProductHandler : IRequestHandler<GetProductQuery, Product>
{
    private readonly IReadOnlyApplicationDbContext _context;

    public GetProductHandler(IReadOnlyApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var productId = request.ProductId;
        var foundProduct = await _context.Products.FirstOrDefaultAsync(product => product.Id == productId, cancellationToken);

        return foundProduct ?? throw new KeyNotFoundException($"Product with id '{request.ProductId}' was not found.");
    }
}
