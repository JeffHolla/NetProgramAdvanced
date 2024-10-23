using Asp.Versioning;
using CartService.BLL.CartLogic;
using CartService.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CartService.PL.WebAPI.Controllers.V2;

[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/carts")]
[ApiController]
[Produces("application/json", "application/xml")]
[Consumes("application/json", "application/xml")]
public class CartsController(ICartLogicHandler cartLogic) : ControllerBase
{
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCarts()
    {
        var carts = await cartLogic.GetAllCartsAsync();

        return Ok(carts);
    }
    
    // New version of method
    [HttpGet("{cartId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCartInfo(string cartId)
    {
        var cart = await cartLogic.GetCartAsync(cartId);

        return Ok(cart.Items);
    }

    [HttpPost("{cartId}/items")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddItmeToCart(string cartId, [FromBody] ProductItem productItem)
    {
        await cartLogic.AddItemToCartAsync(cartId, productItem);
        return Ok();
    }

    [HttpDelete("{cartId}/items/{itemId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddItmeToCart(string cartId, int itemId)
    {
        await cartLogic.RemoveItemFromCartAsync(cartId, itemId);
        return Ok();
    }
}