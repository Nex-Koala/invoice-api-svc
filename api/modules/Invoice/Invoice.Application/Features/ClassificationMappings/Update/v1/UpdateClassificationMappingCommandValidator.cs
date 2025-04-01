using FluentValidation;

namespace NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Update.v1;

public class UpdateClassificationMappingCommandValidator : AbstractValidator<UpdateClassificationMappingCommand>
{
    public UpdateClassificationMappingCommandValidator()
    {
        RuleFor(x => x.LhdnClassificationCode).NotEmpty().WithMessage("LHDN UOM Code is required.");
        RuleFor(x => x.ClassificationId).NotEmpty().WithMessage("UOM ID is required.");
    }
}
