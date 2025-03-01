using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.InvoiceDocuments.Commands.GenerateInvoice
{
    public class GenerateInvoiceCommandValidator : AbstractValidator<GenerateInvoiceCommand>
    {
        public GenerateInvoiceCommandValidator()
        {
            RuleFor(x => x.Uuid)
                .NotEmpty().WithMessage("Uuid is a required.");  // Ensure Uuid is not empty 
        }
    }
}
