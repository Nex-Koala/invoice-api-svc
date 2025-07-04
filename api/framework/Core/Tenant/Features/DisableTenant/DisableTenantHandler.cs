﻿using MediatR;
using NexKoala.Framework.Core.Tenant.Abstractions;

namespace NexKoala.Framework.Core.Tenant.Features.DisableTenant;
public sealed class DisableTenantHandler(ITenantService service) : IRequestHandler<DisableTenantCommand, DisableTenantResponse>
{
    public async Task<DisableTenantResponse> Handle(DisableTenantCommand request, CancellationToken cancellationToken)
    {
        var status = await service.DeactivateAsync(request.TenantId);
        return new DisableTenantResponse(status);
    }
}
