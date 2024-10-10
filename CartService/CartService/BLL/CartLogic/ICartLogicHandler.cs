using CartService.Common.Entities;

namespace CartService.BLL.CartLogic
{
    public interface ICartLogicHandler
    {
        Task<IReadOnlyCollection<ProductItem>> GetCartItemsAsync(int cartId);
        Task AddItemToCartAsync(int cartId, ProductItem productItem);
        Task RemoveItemFromCartAsync(int cartId, int itemId);
    }
}