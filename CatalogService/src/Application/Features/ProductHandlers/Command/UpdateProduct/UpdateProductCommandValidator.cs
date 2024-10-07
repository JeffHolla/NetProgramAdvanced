namespace CatalogService.Application.Features.ProductHandlers.Command.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(category => category.NewName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(category => category.NewCategory)
            .NotEmpty();
    }
}
