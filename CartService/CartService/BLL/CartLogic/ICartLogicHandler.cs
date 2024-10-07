using CartService.Common.Entities;

namespace CartService.BLL.CartLogic
{
    public interface ICartLogicHandler
    {
        IReadOnlyCollection<ProductItem> GetCartItems(int cartId);
        void AddItemToCart(int cartId, ProductItem productItem);
        void RemoveItemFromCart(int cartId, int itemId);
    }
}