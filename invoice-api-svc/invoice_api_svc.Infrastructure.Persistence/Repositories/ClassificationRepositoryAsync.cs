using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using invoice_api_svc.Domain.Entities;
using invoice_api_svc.Infrastructure.Persistence.Contexts;
using invoice_api_svc.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Infrastructure.Persistence.Repositories
{
    public class ClassificationRepositoryAsync : GenericRepositoryAsync<Classification>, IClassificationRepositoryAsync
    {
        private readonly DbSet<Classification> _classifications;

        public ClassificationRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _classifications = dbContext.Set<Classification>();
        }

        public async Task<bool> IsCodeUniqueAsync(string code)
        {
            return !await _classifications.AnyAsync(u => u.Code == code && !u.IsDeleted);
        }

        public async Task<PagedResponse<IReadOnlyList<Classification>>> GetPagedReponseAsync(int pageNumber, int pageSize, Guid userId)
        {
            var query = _classifications.Where(u => !u.IsDeleted && u.UserId == userId);

            var totalCount = await query.CountAsync();

            IReadOnlyList<Classification> classifications;

            if (pageNumber > 0 && pageSize > 0)
            {
                classifications = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            else
            {
                classifications = await query
                    .ToListAsync();
            }

            return new PagedResponse<IReadOnlyList<Classification>>(classifications, pageNumber, pageSize, totalCount);
        }
    }
}
