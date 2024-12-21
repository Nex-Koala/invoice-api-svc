using invoice_api_svc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Interfaces.Repositories
{
    public interface IUomRepositoryAsync : IGenericRepositoryAsync<Uom>
    {
        Task<bool> IsCodeUniqueAsync(string code);
        Task<IReadOnlyList<Uom>> GetPagedReponseAsync(int pageNumber, int pageSize, Guid UserId);
    }
}
