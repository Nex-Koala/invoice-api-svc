using invoice_api_svc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Interfaces.Repositories
{
    public interface IClassificationMappingRepositoryAsync : IGenericRepositoryAsync<ClassificationMapping>
    {
        Task<IReadOnlyList<ClassificationMapping>> GetMappingsByClassificationIdAsync(int classificationId);

        Task<IReadOnlyList<ClassificationMapping>> GetMappingsByUserIdAsync(Guid userId);
    }
}
