using Asp.Versioning;
using CartService.BLL.CartLogic;
using Microsoft.AspNetCore.Mvc;

namespace CartService.PL.WebAPI.Controllers.V2;

[ApiController, ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/carts")]
[Produces("application/json")]
[Consumes("application/json")]
public class CartsController(ICartLogicHandler cartLogic) : ControllerBase
{
    /// <summary>    
    ///     Returns information about the items in the selected cart.
    /// </summary>    
    /// <remarks>    
    /// Sample request:    
    ///    
    ///     GET /api/v1/carts/1
    ///    
    /// </remarks>
    /// <param name="cartId">The ID of the cart whose items you want to get information about</param>
    /// <returns>    
    ///     An array containing information about the items in the selected cart.
    /// </returns> 
    [HttpGet("{cartId}"), MapToApiVersion(2.0)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCartInfo(string cartId)
    {
        var cart = await cartLogic.GetCartAsync(cartId);

        return Ok(cart.Items);
    }
}