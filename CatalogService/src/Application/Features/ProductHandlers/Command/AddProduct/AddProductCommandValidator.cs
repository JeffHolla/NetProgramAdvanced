namespace CatalogService.Application.Features.ProductHandlers.Command.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(product => product.Name)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(product => product.CategoryId)
            .NotNull();

        RuleFor(product => product.Amount)
            .NotNull();

        RuleFor(product => product.Price)
            .NotNull();
    }
}
