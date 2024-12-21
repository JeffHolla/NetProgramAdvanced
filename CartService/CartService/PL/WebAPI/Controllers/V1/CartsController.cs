using Asp.Versioning;
using CartService.BLL.CartLogic;
using CartService.Common.Entities;
using CartService.Common.Security.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartService.PL.WebAPI.Controllers.V1;

[ApiController, ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/carts")]
[Produces("application/json")]
[Consumes("application/json")]
//[Authorize(Roles = $"{ApplicationRoles.Manager}, {ApplicationRoles.StoreCustomer}")]
public class CartsController(ICartLogicHandler cartLogic) : ControllerBase
{
    /// <summary>    
    ///     Returns information about all carts.
    /// </summary>
    /// <example>
    ///     Some Example hehe
    /// </example>
    /// <remarks>    
    /// Sample request:    
    ///    
    ///     GET /api/v1/carts
    ///    
    /// </remarks>
    /// <returns>    
    ///     An array containing all information about the carts and the items in them.
    /// </returns> 
    [HttpGet]
    [MapToApiVersion(1.0), MapToApiVersion(2.0)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCarts()
    {
        var carts = await cartLogic.GetAllCartsAsync();

        return Ok(carts);
    }

    /// <summary>    
    ///     Returns information about the selected cart.
    /// </summary>    
    /// <remarks>    
    /// Sample request:    
    ///    
    ///     GET /api/v1/carts/1
    ///    
    /// </remarks>
    /// <param name="cartId">The ID of the cart you want to receive</param>
    /// <returns>    
    ///     An object representing information about a cart.
    /// </returns> 
    [HttpGet("{cartId}")]
    [ProducesResponseType<Cart>(StatusCodes.Status200OK)]
    public IActionResult GetCartInfo(string cartId)
    {
        var cart = cartLogic.GetCartAsync(cartId);

        return Ok(cart);
    }

    /// <summary>    
    ///     Adds an item to the cart.
    /// </summary>    
    /// <remarks>    
    /// Sample request:    
    /// 
    ///     POST /api/carts/1/items
    ///     {
    ///         "Id": 1,
    ///         "Name": "Item name",
    ///         "Image": "Image url",
    ///         "Price": 25,
    ///         "Quantity": 3
    ///     }
    ///    
    /// </remarks>
    /// <param name="cartId">The ID of the cart you want to add the item to</param>
    /// <param name="productItem">The item you want to add to the cart</param>
    /// <returns>    
    ///     The result of request processing as an HTTP status.
    /// </returns> 
    [HttpPost("{cartId}/items")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddItmeToCart(string cartId, [FromBody] ProductItem productItem)
    {
        await cartLogic.AddItemToCartAsync(cartId, productItem);
        return Ok();
    }

    /// <summary>    
    ///     Adds an item to the cart.
    /// </summary>    
    /// <remarks>    
    /// Sample request:    
    /// 
    ///     DELETE /api/carts/1/items/2
    ///    
    /// </remarks>
    /// <param name="cartId">The ID of the cart you want to delete the item from</param>
    /// <param name="itemId">The item ID you want to delete from the cart</param>
    /// <returns>    
    ///     The result of request processing as an HTTP status.
    /// </returns> 
    [HttpDelete("{cartId}/items/{itemId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddItmeToCart(string cartId, int itemId)
    {
        await cartLogic.RemoveItemFromCartAsync(cartId, itemId);
        return Ok();
    }
}
