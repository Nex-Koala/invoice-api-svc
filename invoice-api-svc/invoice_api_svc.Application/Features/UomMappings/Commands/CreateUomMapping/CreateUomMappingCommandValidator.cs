using FluentValidation;

namespace invoice_api_svc.Application.Features.UomMappings.Commands.CreateUomMapping
{
    public class CreateUomMappingCommandValidator : AbstractValidator<CreateUomMappingCommand>
    {
        public CreateUomMappingCommandValidator()
        {
            RuleFor(x => x.LhdnUomCode).NotEmpty().WithMessage("LHDN UOM Code is required.");
            RuleFor(x => x.UomId).GreaterThan(0).WithMessage("UOM ID must be greater than 0.");
        }
    }
}
