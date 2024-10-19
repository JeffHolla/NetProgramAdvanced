using CatalogService.Application.Features.ProductHandlers.Queries.GetAllProducts;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.FunctionalTests.CategoryHandlers.Queries;

using static Testing;

public class GetAllProductsTests : BaseTestFixture
{
    [Test]
    public async Task AllProducts_Should_ReturnEmptyCollectionWhenDbIsEmpty()
    {
        var query = new GetAllProductsQuery();

        var result = await SendAsync(query);

        result.Results.Should().BeEmpty();
    }

    [Test]
    public async Task AllProducts_Should_ReturnNotAnEmptyCollectionWhenDbIsEmpty()
    {
        var firstCategory = new Category()
        {
            Name = "Cat Name"
        };

        var product1 = new Product
        {
            Name = "1",
            Category = firstCategory,
            Description = "1",
            Price = 1,
            Amount = 1
        };

        var product2 = new Product
        {
            Name = "2",
            Category = firstCategory,
            Description = "2",
            Price = 2,
            Amount = 2
        };
        var expectedProducts = new List<Product>() { product1, product2 };
        await AddAsync(firstCategory);
        await AddAsync(product1);
        await AddAsync(product2);

        var query = new GetAllProductsQuery();

        var result = await SendAsync(query);

        result.Results.Should().BeEquivalentTo(expectedProducts);
    }
}
