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

        public async Task<IReadOnlyCollection<ProductItem>> GetCartItemsAsync(int cartId)
        {
            var cart = await _cartRepository.GetEntityAsync(cartId);
            return cart.Items.AsReadOnly();
        }

        public async Task AddItemToCartAsync(int cartId, ProductItem productItem)
        {
            var validationResult = new CartItemValidator().Validate(productItem);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(failure => failure.ErrorMessage);
                var errorMessagesString = string.Join(Environment.NewLine, errorMessages);
                throw new ValidationFailedException(errorMessagesString);
            }

            var cart = await _cartRepository.GetEntityAsync(cartId);
            cart.AddItem(productItem);

            await _cartRepository.UpdateEntityAsync(cart.Id, cart);
        }

        public async Task RemoveItemFromCartAsync(int cartId, int itemId)
        {
            var cart = await _cartRepository.GetEntityAsync(cartId);
            cart.RemoveItem(itemId);

            await _cartRepository.UpdateEntityAsync(cart.Id, cart);
        }
    }
}
