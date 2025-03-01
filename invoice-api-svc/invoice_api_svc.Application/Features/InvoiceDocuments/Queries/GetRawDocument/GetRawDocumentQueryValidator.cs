using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.InvoiceDocuments.Queries.GetRawDocument
{
    public class GetRawDocuemntQueryValidator : AbstractValidator<GetRawDocuemntQuery>
    {
        public GetRawDocuemntQueryValidator()
        {
            RuleFor(x => x.Uuid)
                .NotEmpty().WithMessage("Uuid is a required.");  // Ensure Uuid is not empty 
        }
    }
}
