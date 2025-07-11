using FluentValidation;

namespace CustomerManagement.Application.Dtos.CommunicationTemplates.Validation
{
    public class CreateUpdateTemplateDtoValidator : AbstractValidator<CreateUpdateTemplateDto>
    {
        public CreateUpdateTemplateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");
            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Subject is required.");
            RuleFor(x => x.Body).NotEmpty().WithMessage("Body is required.");
        }
    }
}
