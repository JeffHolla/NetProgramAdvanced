using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Features.ProductHandlers.Command.AddProduct;

public class AddProductCommand : IRequest<Product>
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public string? Image { get; init; }
    public int? CategoryId { get; init; }
    public decimal? Price { get; init; }
    public int? Amount { get; init; }
}

public class UpdateProductHandler : IRequestHandler<AddProductCommand, Product>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    // Validation has been executed in MediatR Behaviours
    public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var requestCategoryId = request.CategoryId;
        var foundCategory = await _context.Categories.FirstOrDefaultAsync(category => category.Id == requestCategoryId, cancellationToken);
        if (foundCategory == null)
        {
            throw new KeyNotFoundException($"Category with id '{requestCategoryId}' was not found.");
        }

        var newProduct = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Image = request.Image,
            Category = foundCategory,
            Price = request.Price.Value,
            Amount = request.Amount.Value
        };

        _context.Products.Add(newProduct);

        await _context.SaveChangesAsync(cancellationToken);

        return newProduct;
    }
}
