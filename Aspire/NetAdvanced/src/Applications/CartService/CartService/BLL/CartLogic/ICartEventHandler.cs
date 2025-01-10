using CartService.BLL.CartLogic.Events;

namespace CartService.BLL.CartLogic
{
    public interface ICartEventHandler
    {
        Task UpdateItemInAllCartsAsync(UpdateProductEvent productEvent);
    }
}
