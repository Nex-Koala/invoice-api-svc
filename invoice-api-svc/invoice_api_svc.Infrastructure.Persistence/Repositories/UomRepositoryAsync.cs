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
    public class UomRepositoryAsync : GenericRepositoryAsync<Uom>, IUomRepositoryAsync
    {
        private readonly DbSet<Uom> _uoms;

        public UomRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _uoms = dbContext.Set<Uom>();
        }

        public async Task<bool> IsCodeUniqueAsync(string code)
        {
            return !await _uoms.AnyAsync(u => u.Code == code && !u.IsDeleted);
        }

        public async Task<IReadOnlyList<Uom>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _uoms
                .Where(u => !u.IsDeleted)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
