using CartService.Common.Entities;
using FluentValidation;

namespace CartService.BLL.CartLogic
{
    public class CartItemValidator : AbstractValidator<ProductItem>
    {
        public CartItemValidator()
        {
            RuleFor(item => item.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(item => item.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(item => item.Price)
                .NotEmpty().WithMessage("Price is required.");
        }
    }
}
