using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Features.CategoryHandlers.Command.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public int CategoryId { get; set; }
    public string NewName { get; init; }
    public string? NewImage { get; init; }
    public Category? NewParent { get; init; }
}

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    // Validation has been executed in MediatR Behaviours
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryId = request.CategoryId;
        var existedCategory = await _context.Categories.FirstOrDefaultAsync(category => category.Id == categoryId, cancellationToken);

        if (existedCategory is null)
        {
            throw new KeyNotFoundException($"Category with id '{request.CategoryId}' was not found.");
        }

        var updatedCategory = new Category()
        {
            Image = request.NewImage,
            Parent = request.NewParent,
            Name = request.NewName
        };

        existedCategory.Update(updatedCategory);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
