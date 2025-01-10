using System.Text.Json;
using CatalogService.Application.Common.Interfaces.Database;
using CatalogService.Application.Common.Interfaces.Services;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Features.ProductHandlers.Command.UpdateProduct;

public class UpdateProductCommand : IRequest
{
    public int? ProductId { get; set; }
    public string? NewName { get; init; }
    public string? NewDescription { get; init; }
    public string? NewImage { get; init; }
    public int? NewCategoryId { get; init; }
    public decimal? NewPrice { get; init; }
    public int? NewAmount { get; init; }
}

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICartClientService _cartService;

    public UpdateProductHandler(IApplicationDbContext context, ICartClientService cartService)
    {
        _context = context;
        _cartService = cartService;
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
            Name = request.NewName ?? existedProduct.Name,
            Description = request.NewDescription ?? existedProduct.Description,
            CategoryId = request.NewCategoryId ?? existedProduct.CategoryId,
            Price = request.NewPrice ?? existedProduct.Price,
            Amount = request.NewAmount ?? existedProduct.Amount,
            Image = request.NewImage ?? existedProduct.Image
        };

        existedProduct.Update(updatedProduct);
        await _context.SaveChangesAsync(cancellationToken);

        var message = new UpdateProductEvent(request);
        var messageJson = JsonSerializer.Serialize(message);
        await _cartService.SendMessageToQueue(messageJson, cancellationToken);
    }
}
