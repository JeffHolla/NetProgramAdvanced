using CartService.Common.Messaging;

namespace CartService.BLL.CartLogic.Events
{
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
}
