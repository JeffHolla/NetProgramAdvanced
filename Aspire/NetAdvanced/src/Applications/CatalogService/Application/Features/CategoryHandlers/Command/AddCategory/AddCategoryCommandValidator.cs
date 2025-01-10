namespace CatalogService.Application.Features.CategoryHandlers.Command.AddCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(category => category.Name)
            .MaximumLength(50)
            .NotEmpty();
    }
}
