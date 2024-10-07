using CartService.Common.Entities;
using CartService.Common.Exceptions;
using CartService.DAL.Repositories.Common;

namespace CartService.BLL.CartLogic
{
    public class CartLogicHandler : ICartLogicHandler
    {
        private readonly IRepository<Cart> _cartRepository;

        public CartLogicHandler(IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public IReadOnlyCollection<ProductItem> GetCartItems(int cartId)
        {
            var cart = _cartRepository.GetEntity(cartId);
            return cart.Items.AsReadOnly();
        }

        public void AddItemToCart(int cartId, ProductItem productItem)
        {
            var validationResult = new CartItemValidator().Validate(productItem);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(failure => failure.ErrorMessage);
                var errorMessagesString = string.Join(Environment.NewLine, errorMessages);
                throw new ValidationFailedException(errorMessagesString);
            }

            var cart = _cartRepository.GetEntity(cartId);
            cart.AddItem(productItem);

            _cartRepository.UpdateEntity(cart.Id, cart);
        }

        public void RemoveItemFromCart(int cartId, int itemId)
        {
            var cart = _cartRepository.GetEntity(cartId);
            cart.RemoveItem(itemId);

            _cartRepository.UpdateEntity(cart.Id, cart);
        }
    }
}
