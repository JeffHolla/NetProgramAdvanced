using CatalogService.Application.Common.Interfaces;

namespace CatalogService.Application.Features.CategoryHandlers.Command.DeleteCategory;

public class DeleteCategoryCommand : IRequest
{
    public int CategoryId { get; init; }
}

public class DeleteProductHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryId = request.CategoryId;
        var foundCategory = await _context.Categories.FirstOrDefaultAsync(category => category.Id == categoryId, cancellationToken);

        if (foundCategory is null)
        {
            throw new KeyNotFoundException($"Category with id '{request.CategoryId}' was not found.");
        }

        _context.Categories.Remove(foundCategory);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
