using FluentValidation;

namespace NexKoala.WebApi.Invoice.Application.Features.Uoms.Update.v1;

public class UpdateUomCommandValidator : AbstractValidator<UpdateUomCommand>
{
    public UpdateUomCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
    }
}
