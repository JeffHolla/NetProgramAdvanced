using CartService.BLL.CartLogic.Events;
using CartService.Common.Entities;
using CartService.DAL.Repositories.Common;

namespace CartService.BLL.CartLogic
{
    public class CartEventHandler : ICartEventHandler
    {
        private readonly IRepository<Cart> _cartRepository;

        public CartEventHandler(IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task UpdateItemInAllCartsAsync(UpdateProductEvent productEvent)
        {
            var updatedProductId = productEvent.ProductId;
            var carts = await _cartRepository.GetAllEntitiesAsync();

            foreach (var cart in carts)
            {
                foreach (var item in cart.Items.Where(item => item.Id == updatedProductId))
                {
                    item.Quantity = productEvent.NewAmount ?? item.Quantity;
                    item.Price = productEvent.NewPrice ?? item.Price;
                    item.Name = productEvent.NewName ?? item.Name;
                    item.Image = productEvent.NewImage ?? item.Image;
                }

                await _cartRepository.UpdateEntityAsync(cart.Id, cart);
            }
        }
    }
}
