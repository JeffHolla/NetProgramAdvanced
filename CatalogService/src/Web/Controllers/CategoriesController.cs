using CatalogService.Application.Features.CategoryHandlers.Command.AddCategory;
using CatalogService.Application.Features.CategoryHandlers.Command.DeleteCategory;
using CatalogService.Application.Features.CategoryHandlers.Command.UpdateCategory;
using CatalogService.Application.Features.CategoryHandlers.Queries.GetAllCategories;
using CatalogService.Application.Features.CategoryHandlers.Queries.GetCategory;
using CatalogService.Domain.Entities;
using CatalogService.Web.RestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Web.Controllers;

[Route("api/categories")]
[ApiController]
[Produces("application/json", "application/xml")]
[Consumes("application/json", "application/xml")]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories()
    {
        var query = new GetAllCategoriesQuery();

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{categoryId}", Name = nameof(GetCategory))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategory(int categoryId)
    {
        var query = new GetCategoryQuery()
        {
            CategoryId = categoryId
        };

        var result = await mediator.Send(query);

        return Ok(result);
    }

    // An example of the Third maturity level 
    [HttpPost(Name = nameof(AddCategory))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryCommand command)
    {
        var result = await mediator.Send(command);

        var relatedLink = Url.Link(nameof(GetCategory), new { categoryId = result.Id });
        var categoryResourceLink = new ResourceLink(relatedLink, "self", HttpMethod.Get.Method);

        var response = new RestResponse<Category>()
        {
            Links = [categoryResourceLink],
            Response = result
        };

        return Created(relatedLink, response);
    }

    // An example of the Third maturity level 
    [HttpPut("{categoryId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] UpdateCategoryCommand command)
    {
        command.CategoryId = categoryId;
        await mediator.Send(command);

        var relatedLink = Url.Link(nameof(GetCategory), new { categoryId });
        var categoryResourceLink = new ResourceLink(relatedLink, "self", HttpMethod.Get.Method);

        var response = new RestResponse<object>()
        {
            Links = [categoryResourceLink]
        };
        return Ok(response);
    }

    [HttpDelete("{categoryId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCategory(int categoryId)
    {
        var command = new DeleteCategoryCommand()
        {
            CategoryId = categoryId
        };

        await mediator.Send(command);

        return NoContent();
    }
}
