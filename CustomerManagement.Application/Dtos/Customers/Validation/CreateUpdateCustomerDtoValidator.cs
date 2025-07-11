using FluentValidation;

namespace CustomerManagement.Application.Dtos.Customers.Validation
{
    public class CreateUpdateCustomerDtoValidator : AbstractValidator<CreateUpdateCustomerDto>
    {
        public CreateUpdateCustomerDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email format.");
        }
    }
}