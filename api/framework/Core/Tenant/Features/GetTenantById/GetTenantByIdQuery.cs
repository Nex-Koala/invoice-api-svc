using NexKoala.Framework.Core.Tenant.Dtos;
using MediatR;

namespace NexKoala.Framework.Core.Tenant.Features.GetTenantById;
public record GetTenantByIdQuery(string TenantId) : IRequest<TenantDetail>;
