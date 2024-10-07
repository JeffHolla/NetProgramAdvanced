using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Features.ProductHandlers.Command.UpdateProduct;

public class UpdateProductCommand : IRequest
{
    public int ProductId { get; init; }
    public string NewName { get; init; }
    public string? NewDescription { get; init; }
    public string? NewImage { get; init; }
    public Category NewCategory { get; init; }
    public decimal NewPrice { get; init; }
    public uint NewAmount { get; init; }
}

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    // Validation has been executed in MediatR Behaviours
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productId = request.ProductId;
        var existedProduct = await _context.Products.FirstOrDefaultAsync(product => product.Id == productId, cancellationToken);

        if (existedProduct is null)
        {
            throw new KeyNotFoundException($"Product with id '{request.ProductId}' was not found.");
        }

        var updatedProduct = new Product()
        {
            Name = request.NewName,
            Description = request.NewDescription,
            Category = request.NewCategory,
            Price = request.NewPrice,
            Amount = request.NewAmount,
            Image = request.NewImage
        };

        existedProduct.Update(updatedProduct);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
