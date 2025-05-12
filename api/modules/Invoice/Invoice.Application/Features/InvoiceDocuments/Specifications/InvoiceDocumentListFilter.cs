using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using NexKoala.Framework.Core.Paging;
using NexKoala.Framework.Core.Specifications;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Get.v1;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Specifications;

internal class InvoiceDocumentListFilterSpec
    : EntitiesByPaginationFilterSpec<InvoiceDocument, InvoiceDocumentResponse>
{
    public InvoiceDocumentListFilterSpec(
        int pageNumber,
        int pageSize,
        string? uuid = null,
        bool? status = null,
        DocumentStatus? documentStatus = null,
        DateTime? issueDateFrom = null,
        DateTime? issueDateTo = null,
        Guid? userId = null
    )
        : base(new PaginationFilter { PageNumber = pageNumber, PageSize = pageSize })
    {
        Query.Where(i =>
            (uuid == null || (i.Uuid != null && i.Uuid.Contains(uuid)))
            && (status == null || i.SubmissionStatus == status)
            && (documentStatus == null || i.DocumentStatus == documentStatus)
            && (issueDateFrom == null || i.IssueDate >= issueDateFrom)
            && (issueDateTo == null || i.IssueDate <= issueDateTo)
            && (userId == null || userId == Guid.Empty || i.CreatedBy == userId)
        ).OrderByDescending(i => i.IssueDate);
    }
}
