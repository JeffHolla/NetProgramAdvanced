using CatalogService.Application.Features.CategoryHandlers.Queries.GetCategory;
using CatalogService.Application.Features.ProductHandlers.Command.AddProduct;
using CatalogService.Application.Features.ProductHandlers.Command.DeleteProduct;
using CatalogService.Application.Features.ProductHandlers.Command.UpdateProduct;
using CatalogService.Application.Features.ProductHandlers.Queries.GetAllProducts;
using CatalogService.Domain.Entities;
using CatalogService.Web.RestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Web.Controllers;

[Route("api/products")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json", "application/xml")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts()
    {
        var query = new GetAllProductsQuery();

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{productId}", Name = nameof(GetProduct))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProduct(int productId)
    {
        var query = new GetCategoryQuery()
        {
            CategoryId = productId
        };

        var result = await mediator.Send(query);

        return Ok(result);
    }

    // An example of the Third maturity level 
    [HttpPost(Name = nameof(AddProduct))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddProduct([FromBody] AddProductCommand command)
    {
        var result = await mediator.Send(command);

        var relatedLink = Url.Link(nameof(GetProduct), new { productId = result.Id });
        var categoryResourceLink = new ResourceLink(relatedLink, "self", HttpMethod.Get.Method);

        var response = new RestResponse<Product>()
        {
            Links = [categoryResourceLink],
            Response = result
        };

        return Created(relatedLink, response);
    }

    // An example of the Third maturity level 
    [HttpPut("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductCommand command)
    {
        command.ProductId = productId;
        await mediator.Send(command);

        var relatedLink = Url.Link(nameof(GetProduct), new { productId });
        var categoryResourceLink = new ResourceLink(relatedLink, "self", HttpMethod.Get.Method);

        var response = new RestResponse<object>()
        {
            Links = [categoryResourceLink]
        };
        return Ok(response);
    }

    [HttpDelete("{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCategory(int productId)
    {
        var command = new DeleteProductCommand()
        {
            ProductId = productId
        };

        await mediator.Send(command);

        return NoContent();
    }
}
