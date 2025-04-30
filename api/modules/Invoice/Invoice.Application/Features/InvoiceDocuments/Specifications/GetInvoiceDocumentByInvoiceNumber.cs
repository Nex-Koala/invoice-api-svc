using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Specifications;
internal class GetInvoiceDocumentByInvoiceNumber: Specification<InvoiceDocument, InvoiceDocument>
{
    public GetInvoiceDocumentByInvoiceNumber(string invoiceNum, bool getSuccessSubmit = false)
    {
        Query.Where(i => i.InvoiceNumber.ToLower() == invoiceNum.ToLower());

        if (getSuccessSubmit)
        {
            Query.Where(i => i.SubmissionStatus);
        }
        
    }
    
}
