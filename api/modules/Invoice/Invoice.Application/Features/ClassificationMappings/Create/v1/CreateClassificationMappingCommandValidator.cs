using FluentValidation;

namespace NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Create.v1;

public class CreateClassificationMappingCommandValidator : AbstractValidator<CreateClassificationMappingCommand>
{
    public CreateClassificationMappingCommandValidator()
    {
        RuleFor(x => x.LhdnClassificationCode).NotEmpty().WithMessage("LHDN Classification Code is required.");
        RuleFor(x => x.ClassificationId).NotEmpty().WithMessage("Classificaiton ID is required.");
    }
}
