using FluentValidation;

namespace invoice_api_svc.Application.Features.ClassificationMapping.Commands.CreateClassificationMapping
{
    public class CreateClassificationMappingCommandValidator : AbstractValidator<CreateClassificationMappingCommand>
    {
        public CreateClassificationMappingCommandValidator()
        {
            RuleFor(x => x.LhdnClassificationCode).NotEmpty().WithMessage("LHDN Classification Code is required.");
            RuleFor(x => x.ClassificationId).GreaterThan(0).WithMessage("Classification ID must be greater than 0.");
        }
    }
}
