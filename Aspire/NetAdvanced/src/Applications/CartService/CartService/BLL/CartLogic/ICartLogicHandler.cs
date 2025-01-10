using CartService.Common.Entities;

namespace CartService.BLL.CartLogic
{
    public interface ICartLogicHandler
    {
        Task<IEnumerable<Cart>> GetAllCartsAsync();
        Task<Cart> GetCartAsync(string cartId);
        Task AddItemToCartAsync(string cartId, ProductItem productItem);
        Task RemoveItemFromCartAsync(string cartId, int itemId);
    }
}