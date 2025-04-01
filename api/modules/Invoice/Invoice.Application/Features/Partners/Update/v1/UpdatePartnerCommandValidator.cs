using FluentValidation;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Update.v1;

public class UpdatePartnerCommandValidator : AbstractValidator<UpdatePartnerCommand>
{
    public UpdatePartnerCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
    }
}
