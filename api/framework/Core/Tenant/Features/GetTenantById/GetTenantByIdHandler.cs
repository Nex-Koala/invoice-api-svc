using MediatR;
using NexKoala.Framework.Core.Tenant.Dtos;
using NexKoala.Framework.Core.Tenant.Abstractions;

namespace NexKoala.Framework.Core.Tenant.Features.GetTenantById;
public sealed class GetTenantByIdHandler(ITenantService service) : IRequestHandler<GetTenantByIdQuery, TenantDetail>
{
    public async Task<TenantDetail> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
    {
        return await service.GetByIdAsync(request.TenantId);
    }
}
