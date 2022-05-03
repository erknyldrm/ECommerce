using ECommerce.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerce.Application.Validators.Products
{
    public class CreateProductValidator: AbstractValidator<VMCreateProduct>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Please enter product name")
                .MaximumLength(150)
                .MinimumLength(5)
                    .WithMessage("Please enter product character between 5-150");

            RuleFor(x => x.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter stock info");
        }
    }
}
