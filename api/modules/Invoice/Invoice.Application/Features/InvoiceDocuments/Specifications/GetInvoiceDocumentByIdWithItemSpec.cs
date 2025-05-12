using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Get.v1;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Specifications;
internal class GetInvoiceDocumentByIdWithItemSpec : Specification<InvoiceDocument, InvoiceDocumentResponse>
{
    public GetInvoiceDocumentByIdWithItemSpec(Guid id)
    {
        Query.Where(i => i.Id == id);
    }

}
