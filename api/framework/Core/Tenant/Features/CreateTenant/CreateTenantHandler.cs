using MediatR;
using NexKoala.Framework.Core.Tenant.Abstractions;

namespace NexKoala.Framework.Core.Tenant.Features.CreateTenant;
public sealed class CreateTenantHandler(ITenantService service) : IRequestHandler<CreateTenantCommand, CreateTenantResponse>
{
    public async Task<CreateTenantResponse> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenantId = await service.CreateAsync(request, cancellationToken);
        return new CreateTenantResponse(tenantId);
    }
}
