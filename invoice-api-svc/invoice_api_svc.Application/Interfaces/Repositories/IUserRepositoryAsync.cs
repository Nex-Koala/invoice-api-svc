using invoice_api_svc.Application.Features.Partners.Queries.GetAllPartners;
using invoice_api_svc.Application.Wrappers;
using invoice_api_svc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Interfaces.Repositories
{
    public interface IUserRepositoryAsync: IGenericRepositoryAsync<User>
    {
        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);
        Task<PagedResponse<IReadOnlyList<User>>> GetPagedResponseAsync(GetAllPartnerFilter filter);
        Task<User> GetByEmailAsync(string email);
    }
}
