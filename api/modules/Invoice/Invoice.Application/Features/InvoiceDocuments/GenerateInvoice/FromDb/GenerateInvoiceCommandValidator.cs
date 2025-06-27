using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GenerateInvoice.FromDb;

public class GenerateInvoiceCommandValidator : AbstractValidator<GenerateInvoiceCommand>
{
    public GenerateInvoiceCommandValidator()
    {
        RuleFor(x => x.Uuid)
            .NotEmpty().WithMessage("Uuid is a required.");
    }
}
