using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Get.v1;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Suppliers.Specifications;
public class SupplierFilterSpec : Specification<Supplier, SupplierDto>
{
    public SupplierFilterSpec(Guid userId)
    {
        Query
            .Where(s => s.CreatedBy == userId)
            .OrderBy(s => s.Name);
    }
}
