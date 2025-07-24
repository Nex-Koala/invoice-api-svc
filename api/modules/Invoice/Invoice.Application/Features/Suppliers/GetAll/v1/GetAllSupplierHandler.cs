using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Get.v1;
using NexKoala.WebApi.Invoice.Application.Features.Suppliers.Specifications;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Suppliers.GetAll.v1;
public sealed class GetAllSupplierHandler(
    [FromKeyedServices("invoice:suppliers")] IReadRepository<Supplier> repository
) : IRequestHandler<GetAllSupplier, Response<List<SupplierDto>>>
{
    public async Task<Response<List<SupplierDto>>> Handle(GetAllSupplier request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var spec = new SupplierFilterSpec(request.UserId);
        var suppliers = await repository.ListAsync(spec, cancellationToken);
        var supplierDistinctByName = suppliers
            .GroupBy(item => new {
                Name = item.Name.ToLower(),
                Tin = item.Tin
            })
            .Select(g => g.First())
            .ToList();

        return new Response<List<SupplierDto>>(supplierDistinctByName);
    }
}
