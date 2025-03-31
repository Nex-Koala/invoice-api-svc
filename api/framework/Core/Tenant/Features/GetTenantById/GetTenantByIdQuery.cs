using MediatR;
using NexKoala.Framework.Core.Tenant.Dtos;

namespace NexKoala.Framework.Core.Tenant.Features.GetTenantById;
public record GetTenantByIdQuery(string TenantId) : IRequest<TenantDetail>;
