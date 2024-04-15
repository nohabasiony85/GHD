using FluentValidation;

namespace ProductManagement.App.Products.CreateProduct;

public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("The Name field is required.");
        RuleFor(x => x.Brand).NotEmpty().WithMessage("The Brand field is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("The Price field must be greater than 0.");
    }
}