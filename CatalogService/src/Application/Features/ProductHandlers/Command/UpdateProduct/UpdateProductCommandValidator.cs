namespace CatalogService.Application.Features.ProductHandlers.Command.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(category => category.NewName)
            .MaximumLength(50);

        RuleFor(category => category.ProductId)
            .NotNull();
    }
}
