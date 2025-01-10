using FluentValidation;

namespace invoice_api_svc.Application.Features.Classification.Commands.CreateClassification
{
    public class CreateClassificationCommandValidator : AbstractValidator<CreateClassificationCommand>
    {
        public CreateClassificationCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        }
    }
}
