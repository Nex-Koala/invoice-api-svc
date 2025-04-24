using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Specifications;
internal class GetInvoiceDocumentByUuid : Specification<InvoiceDocument, InvoiceDocument>
{
    public GetInvoiceDocumentByUuid(string uuid)
    {
        Query.Where(i => i.Uuid == uuid);
    }
}
