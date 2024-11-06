using CatalogService.Application.Common.Interfaces.Database;

namespace CatalogService.Application.Features.ProductHandlers.Command.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public int ProductId { get; init; }
}

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productId = request.ProductId;
        var foundProduct = await _context.Products.FirstOrDefaultAsync(product => product.Id == productId, cancellationToken);

        if (foundProduct is null)
        {
            throw new KeyNotFoundException($"Product with id '{request.ProductId}' was not found.");
        }

        _context.Products.Remove(foundProduct);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
