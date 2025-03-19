using MediatR;

namespace NexKoala.Framework.Core.Tenant.Features.DisableTenant;
public record DisableTenantCommand(string TenantId) : IRequest<DisableTenantResponse>;
