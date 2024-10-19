using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Features.CategoryHandlers.Queries.GetCategory;

public class GetCategoryQuery : IRequest<Category>
{
    public int CategoryId { get; set; }
}

public class GetProductHandler : IRequestHandler<GetCategoryQuery, Category>
{
    private readonly IReadOnlyApplicationDbContext _context;

    public GetProductHandler(IReadOnlyApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Category> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var categoryId = request.CategoryId;
        var foundCategory = await _context.Categories
                                        .Include(category => category.Products)
                                        .FirstOrDefaultAsync(category => category.Id == categoryId, cancellationToken);

        return foundCategory ?? throw new KeyNotFoundException($"Category with id '{request.CategoryId}' was not found.");
    }
}
