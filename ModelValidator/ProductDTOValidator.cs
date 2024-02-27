using FluentValidation;
using FluentValidation.Results;
using Model;

namespace ModelValidator
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(x => x.ProductName)
                .NotNull().WithMessage("Product Name is empty")
                .NotEmpty().WithMessage("Product Name is empty");

            RuleFor(x => x.Stock)
                .NotNull().WithMessage("Product Stock is empty")
                .NotEmpty().WithMessage("Product Stock is empty")
                .Must(Number).WithMessage("Product Stock must be a number");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Product Price is empty")
                .NotEmpty().WithMessage("Product Price is empty");
        }

        public override ValidationResult Validate(ValidationContext<ProductDTO> context)
        {
            return context.InstanceToValidate == null
           ? new ValidationResult(new[] { new ValidationFailure("ProductDTOValidator", "Product object not empty") })
           : base.Validate(context);
        }

        private bool Number(int stock)
        {
            // Use int.TryParse to check if the stock value can be parsed as an integer
            return int.TryParse(stock.ToString(), out _);
        }
    }
}
