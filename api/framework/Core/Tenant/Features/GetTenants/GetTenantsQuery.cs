using NexKoala.Framework.Core.Tenant.Dtos;
using MediatR;

namespace NexKoala.Framework.Core.Tenant.Features.GetTenants;
public sealed class GetTenantsQuery : IRequest<List<TenantDetail>>;
