using FluentValidation;
using FluentValidation.Results;
using Model;

namespace ModelValidator
{
    public class CustomerDTOValidator : AbstractValidator<CustomerDTO>
    {
        public CustomerDTOValidator() 
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Customer Name is empty")
                .NotEmpty().WithMessage("Customer Name is empty");
            
            RuleFor(x => x.Contact)
                .NotNull().WithMessage("Mobile Number is empty.")
                .NotEmpty().WithMessage("Mobile Number is empty.")
                .Matches(@"^[0-9]{10}$").WithMessage("Mobile Number is invalid.");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("Customer Email is empty")
                .NotEmpty().WithMessage("Customer Email is empty")
                .EmailAddress().WithMessage("Email Address is invalid");
        }

        public override ValidationResult Validate(ValidationContext<CustomerDTO> context)
        {
            return context.InstanceToValidate == null
           ? new ValidationResult(new[] { new ValidationFailure("CustomerDTOValidator", "Customer object not empty") })
           : base.Validate(context);
        }
    }
}
