using System;
using Ardalis.Specification;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Specifications
{
    internal class GetInvoiceDocumentByDocumentStatusSpec : Specification<InvoiceDocument>
    {
        public GetInvoiceDocumentByDocumentStatusSpec(DocumentStatus status, DateTime? startDate = null, DateTime? endDate = null, Guid? userId = null)
        {
            Query.Where(i => i.DocumentStatus != null && i.DocumentStatus == status);

            if (startDate.HasValue)
            {
                Query.Where(i => i.IssueDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                Query.Where(i => i.IssueDate <= endDate.Value);
            }

            if (userId.HasValue && userId != Guid.Empty)
            {
                Query.Where(i => i.CreatedBy == userId.Value);
            }
        }
    }
}
