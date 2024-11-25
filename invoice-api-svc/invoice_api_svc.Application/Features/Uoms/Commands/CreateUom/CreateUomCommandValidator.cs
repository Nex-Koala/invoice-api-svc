using FluentValidation;
using invoice_api_svc.Application.Features.Uoms.Commands.CreateUom;

namespace invoice_api_svc.Application.Features.Products.Commands.CreateUom
{
    public class CreateUomCommandValidator : AbstractValidator<CreateUomCommand>
    {
        public CreateUomCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        }
    }
}
