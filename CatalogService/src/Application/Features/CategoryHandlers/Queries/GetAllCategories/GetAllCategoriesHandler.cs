using CatalogService.Application.Common.Interfaces;
using CatalogService.Application.Common.Models;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Features.CategoryHandlers.Queries.GetAllCategories;

public class GetAllCategoriesQuery : IRequest<ListResponse<Category>> { }

public class GetAllProductsHandler : IRequestHandler<GetAllCategoriesQuery, ListResponse<Category>>
{
    private readonly IReadOnlyApplicationDbContext _context;

    public GetAllProductsHandler(IReadOnlyApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ListResponse<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories
                                        .Include(category => category.Products)
                                        .ToListAsync(cancellationToken);
        return new ListResponse<Category>(categories);
    }
}
