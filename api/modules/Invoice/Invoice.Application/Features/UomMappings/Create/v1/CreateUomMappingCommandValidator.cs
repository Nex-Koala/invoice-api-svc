using FluentValidation;

namespace NexKoala.WebApi.Invoice.Application.Features.UomMappings.Create.v1;

public class CreateUomMappingCommandValidator : AbstractValidator<CreateUomMappingCommand>
{
    public CreateUomMappingCommandValidator()
    {
        RuleFor(x => x.LhdnUomCode).NotEmpty().WithMessage("LHDN UOM Code is required.");
        RuleFor(x => x.UomId).NotEmpty().WithMessage("UOM ID is required.");
    }
}
