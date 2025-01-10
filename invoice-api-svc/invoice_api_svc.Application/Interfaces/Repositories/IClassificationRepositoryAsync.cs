using invoice_api_svc.Application.Wrappers;
using invoice_api_svc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Interfaces.Repositories
{
    public interface IClassificationRepositoryAsync : IGenericRepositoryAsync<Classification>
    {
        Task<bool> IsCodeUniqueAsync(string code);
        Task<PagedResponse<IReadOnlyList<Classification>>> GetPagedReponseAsync(int pageNumber, int pageSize, Guid userId);
    }
}
