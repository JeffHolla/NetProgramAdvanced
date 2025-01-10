using CatalogService.Application.Common.Interfaces.Messaging;

namespace CatalogService.Application.Features.ProductHandlers.Command.UpdateProduct;
public class UpdateProductEvent : IEvent
{
    public string Type => nameof(UpdateProductEvent);
    public string Version => "1.0.0";

    public int? ProductId { get; private set; }
    public string? NewName { get; private set; }
    public string? NewDescription { get; private set; }
    public string? NewImage { get; private set; }
    public int? NewCategoryId { get; private set; }
    public decimal? NewPrice { get; private set; }
    public int? NewAmount { get; private set; }

    public UpdateProductEvent(UpdateProductCommand payload)
    {
        ProductId = payload.ProductId;
        NewName = payload.NewName;
        NewDescription = payload.NewDescription;
        NewImage = payload.NewImage;
        NewCategoryId = payload.NewCategoryId;
        NewPrice = payload.NewPrice;
        NewAmount = payload.NewAmount;
    }

    public UpdateProductEvent(
        int? productId,
        string? newName,
        string? newDescription,
        string? newImage,
        int? newCategoryId,
        decimal? newPrice,
        int? newAmount)
    {
        ProductId = productId;
        NewName = newName;
        NewDescription = newDescription;
        NewImage = newImage;
        NewCategoryId = newCategoryId;
        NewPrice = newPrice;
        NewAmount = newAmount;
    }
}
