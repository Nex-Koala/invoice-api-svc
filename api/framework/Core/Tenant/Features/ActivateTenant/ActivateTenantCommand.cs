using MediatR;

namespace NexKoala.Framework.Core.Tenant.Features.ActivateTenant;
public record ActivateTenantCommand(string TenantId) : IRequest<ActivateTenantResponse>;
