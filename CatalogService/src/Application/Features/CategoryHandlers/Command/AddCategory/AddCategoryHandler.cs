using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Features.CategoryHandlers.Command.AddCategory;

public class AddCategoryCommand : IRequest
{
    public string Name { get; init; }
    public string? Image { get; init; }
    public Category? Parent { get; init; }
}

public class UpdateCategoryHandler : IRequestHandler<AddCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    // Validation has been executed in MediatR Behaviours
    public async Task Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = new Category
        {
            Name = request.Name,
            Image = request.Image,
            Parent = request.Parent
        };

        _context.Categories.Add(newCategory);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
