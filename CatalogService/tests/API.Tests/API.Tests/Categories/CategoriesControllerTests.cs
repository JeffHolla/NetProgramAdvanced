using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogService.Application.Common.Models;
using CatalogService.Application.Features.CategoryHandlers.Command.AddCategory;
using CatalogService.Application.Features.CategoryHandlers.Command.DeleteCategory;
using CatalogService.Application.Features.CategoryHandlers.Command.UpdateCategory;
using CatalogService.Application.Features.CategoryHandlers.Queries.GetAllCategories;
using CatalogService.Application.Features.CategoryHandlers.Queries.GetCategory;
using CatalogService.Domain.Entities;
using CatalogService.Web.Controllers;
using CatalogService.Web.RestModels;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace API.Tests.Categories;

[TestFixture]
public class CategoriesControllerTests
{
    private CategoriesController _controller;
    private IMediator _mediator;

    [SetUp]
    public void SetUp()
    {
        _mediator = Substitute.For<IMediator>();
        _controller = new CategoriesController(_mediator);
    }

    [Test]
    public async Task GetCategories_ReturnsOkWithCategories()
    {
        // Arrange
        var categories = new List<Category> { new() { Id = 1, Name = "Electronics" } };
        var listResponse = new ListResponse<Category>(categories);
        _mediator.Send(Arg.Any<GetAllCategoriesQuery>()).Returns(listResponse);

        // Act
        var result = await _controller.GetCategories();

        // Assert
        var okResult = (OkObjectResult)result;
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(listResponse);
    }

    [Test]
    public async Task GetCategory_ReturnsOkWithCategory()
    {
        // Arrange
        var categoryId = 1;
        var category = new Category { Id = categoryId, Name = "Electronics" };
        _mediator.Send(Arg.Any<GetCategoryQuery>()).Returns(category);

        // Act
        var result = await _controller.GetCategory(categoryId);

        // Assert
        var okResult = (OkObjectResult)result;
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(category);
    }

    [Test]
    public async Task AddCategory_ReturnsCreatedResponseWithCategoryLink()
    {
        // Arrange
        var categoryId = 2;
        var category = new Category { Id = categoryId, Name = "New Category" };
        _mediator.Send(Arg.Any<AddCategoryCommand>()).Returns(category);

        var getCategoryLink = $"http://localhost/api/categories/{categoryId}";
        _controller.Url = Substitute.For<IUrlHelper>();
        _controller.Url.Link(nameof(_controller.GetCategory), Arg.Any<object>())
            .Returns(getCategoryLink);

        var resourceLink = new ResourceLink(getCategoryLink, "self", "GET");

        var categoryCommand = new AddCategoryCommand { Name = "New Category" };

        // Act
        var result = await _controller.AddCategory(categoryCommand);

        // Assert
        var createdResult = (CreatedResult)result;
        createdResult.StatusCode.Should().Be(201);
        createdResult.Location.Should().Be(getCategoryLink);

        var response = createdResult.Value as RestResponse<Category>;
        response!.Response.Should().BeEquivalentTo(category);
        response!.Links.Should().BeEquivalentTo([resourceLink]);
    }

    [Test]
    public async Task UpdateCategory_ReturnsOkWithCategoryLink()
    {
        // Arrange
        var categoryId = 1;
        _mediator.Send(Arg.Any<UpdateCategoryCommand>()).Returns(Task.CompletedTask);

        var getCategoryLink = $"http://localhost/api/categories/{categoryId}";
        _controller.Url = Substitute.For<IUrlHelper>();
        _controller.Url.Link(nameof(_controller.GetCategory), Arg.Any<object>())
            .Returns(getCategoryLink);

        var resourceLink = new ResourceLink(getCategoryLink, "self", "GET");

        var updateCommand = new UpdateCategoryCommand { NewName = "Updated Category" };

        // Act
        var result = await _controller.UpdateCategory(categoryId, updateCommand);

        // Assert
        var okResult = (OkObjectResult)result;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);

        var response = okResult.Value as RestResponse<object>;
        response!.Response.Should().BeNull();
        response!.Links.Should().BeEquivalentTo([resourceLink]);
    }

    [Test]
    public async Task DeleteCategory_ReturnsNoContent()
    {
        // Arrange
        var categoryId = 1;
        _mediator.Send(Arg.Any<DeleteCategoryCommand>()).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteCategory(categoryId);

        // Assert
        var noContentResult = (NoContentResult)result;
        noContentResult.Should().NotBeNull();
        noContentResult.StatusCode.Should().Be(204);
    }
}
