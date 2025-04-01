using MediatR;
using NexKoala.Framework.Core.Tenant.Dtos;

namespace NexKoala.Framework.Core.Tenant.Features.GetTenants;
public sealed class GetTenantsQuery : IRequest<List<TenantDetail>>;
