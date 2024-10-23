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

        public async Task<IEnumerable<Cart>> GetAllCartsAsync()
        {
            var carts = await _cartRepository.GetAllEntitiesAsync();

            return carts;
        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
            var cart = await _cartRepository.GetEntityAsync(cartId);

            if (cart == null)
            {
                throw new KeyNotFoundException($"Cart with id '{cartId}' was not found.");
            }

            return cart;
        }

        public async Task AddItemToCartAsync(string cartId, ProductItem productItem)
        {
            var validationResult = new CartItemValidator().Validate(productItem);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(failure => failure.ErrorMessage);
                var errorMessagesString = string.Join(Environment.NewLine, errorMessages);
                throw new ValidationFailedException(errorMessagesString);
            }

            var cart = await GetOrCreateCartAsync(cartId);
            cart.AddItem(productItem);

            await _cartRepository.UpdateEntityAsync(cart.Id, cart);
        }

        public async Task RemoveItemFromCartAsync(string cartId, int itemId)
        {
            var cart = await _cartRepository.GetEntityAsync(cartId);
            cart.RemoveItem(itemId);

            await _cartRepository.UpdateEntityAsync(cart.Id, cart);
        }

        private async Task<Cart> GetOrCreateCartAsync(string cartId)
        {
            var foundCart = await _cartRepository.GetEntityAsync(cartId);
            if (foundCart != null)
            {
                return foundCart;
            }

            var newCart = new Cart
            {
                Id = cartId
            };
            await _cartRepository.AddEntityAsync(newCart);

            return newCart;
        }
    }
}
