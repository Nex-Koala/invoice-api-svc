using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Domain.Entities;
using invoice_api_svc.Infrastructure.Persistence.Contexts;
using invoice_api_svc.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoice_api_svc.Infrastructure.Persistence.Repositories
{
    public class UomMappingRepositoryAsync : GenericRepositoryAsync<UomMapping>, IUomMappingRepositoryAsync
    {
        private readonly DbSet<UomMapping> _uomMappings;

        public UomMappingRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _uomMappings = dbContext.Set<UomMapping>();
        }

        public async Task<IReadOnlyList<UomMapping>> GetMappingsByUomIdAsync(int uomId)
        {
            return await _uomMappings
                .Where(mapping => mapping.UomId == uomId && !mapping.IsDeleted)
                .ToListAsync();
        }
    }
}
