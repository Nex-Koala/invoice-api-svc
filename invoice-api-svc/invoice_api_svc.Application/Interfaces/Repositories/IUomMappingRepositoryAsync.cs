using invoice_api_svc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Interfaces.Repositories
{
    public interface IUomMappingRepositoryAsync : IGenericRepositoryAsync<UomMapping>
    {
        Task<IReadOnlyList<UomMapping>> GetMappingsByUomIdAsync(int uomId);

        Task<IReadOnlyList<UomMapping>> GetMappingsByUserIdAsync(Guid userId);
    }
}
