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
    // New version of method
    [HttpGet("{cartId}"), MapToApiVersion(2.0)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCartInfo(string cartId)
    {
        var cart = await cartLogic.GetCartAsync(cartId);

        return Ok(cart.Items);
    }
}