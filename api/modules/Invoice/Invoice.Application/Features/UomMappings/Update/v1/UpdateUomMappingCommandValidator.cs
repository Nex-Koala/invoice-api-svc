using FluentValidation;

namespace NexKoala.WebApi.Invoice.Application.Features.UomMappings.Update.v1;

public class UpdateUomMappingCommandValidator : AbstractValidator<UpdateUomMappingCommand>
{
    public UpdateUomMappingCommandValidator()
    {
        RuleFor(x => x.LhdnUomCode).NotEmpty().WithMessage("LHDN UOM Code is required.");
        RuleFor(x => x.UomId).NotEmpty().WithMessage("UOM ID is required.");
    }
}
