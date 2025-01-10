namespace CatalogService.Application.Features.CategoryHandlers.Command.UpdateCategory;

public class UpdateProductCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(category => category.NewName)
            .MaximumLength(50)
            .NotEmpty();
    }
}
