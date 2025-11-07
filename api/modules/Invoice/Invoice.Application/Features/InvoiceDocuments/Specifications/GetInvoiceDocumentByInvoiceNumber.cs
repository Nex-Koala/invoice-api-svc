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
    public GetInvoiceDocumentByInvoiceNumber(List<string> invoiceNums, bool getSuccessSubmit = false)
    {
        var loweredInvoiceNums = invoiceNums.Select(x => x.ToLower()).ToList();

        Query.Where(i => loweredInvoiceNums.Contains(i.InvoiceNumber.ToLower()));

        if (getSuccessSubmit)
        {
            Query.Where(i => i.SubmissionStatus);
            Query.Where(i => i.DocumentStatus == DocumentStatus.Submitted && i.DocumentStatus == DocumentStatus.Valid);
        }

    }
}
