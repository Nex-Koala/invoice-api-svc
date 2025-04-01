using FluentValidation;

namespace NexKoala.WebApi.Invoice.Application.Features.Classifications.Create.v1;

public class CreateClassificationCommandValidator : AbstractValidator<CreateClassificationCommand>
{
    public CreateClassificationCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
    }
}
