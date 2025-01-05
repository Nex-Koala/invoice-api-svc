using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using invoice_api_svc.Domain.Entities;
using invoice_api_svc.Infrastructure.Persistence.Contexts;
using invoice_api_svc.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<PagedResponse<IReadOnlyList<Uom>>> GetPagedReponseAsync(int pageNumber, int pageSize, Guid userId)
        {
            var query = _uoms.Where(u => !u.IsDeleted && u.UserId == userId);

            var totalCount = await query.CountAsync();

            IReadOnlyList<Uom> uoms;

            if (pageNumber > 0 && pageSize > 0)
            {
                uoms = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            else
            {
                uoms = await query
                    .ToListAsync();
            }

            return new PagedResponse<IReadOnlyList<Uom>>(uoms, pageNumber, pageSize, totalCount);
        }

    }
}
