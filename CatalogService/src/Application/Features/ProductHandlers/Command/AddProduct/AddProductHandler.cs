using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Features.ProductHandlers.Command.AddProduct;

public class AddProductCommand : IRequest
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public string? Image { get; init; }
    public Category Category { get; init; }
    public decimal Price { get; init; }
    public uint Amount { get; init; }
}

public class UpdateProductHandler : IRequestHandler<AddProductCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    // Validation has been executed in MediatR Behaviours
    public async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Image = request.Image,
            Category = request.Category,
            Price = request.Price,
            Amount = request.Amount
        };

        _context.Products.Add(newProduct);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
