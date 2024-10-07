namespace CatalogService.Application.Features.ProductHandlers.Command.AddProduct;

public class UpdateProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(product => product.Name)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(product => product.Category)
            .NotEmpty();
    }
}
