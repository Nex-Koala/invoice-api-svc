using FluentValidation;

namespace NexKoala.WebApi.Invoice.Application.Features.Classifications.Update.v1;

public class UpdateClassificationCommandValidator : AbstractValidator<UpdateClassificationCommand>
{
    public UpdateClassificationCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
    }
}
