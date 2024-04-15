using FluentValidation;

namespace ProductManagement.App.Products.UpdateProduct;

public class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("The Id field is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("The Name field is required.");
        RuleFor(x => x.Brand).NotEmpty().WithMessage("The Brand field is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("The Price field must be greater than 0.");
    }
}