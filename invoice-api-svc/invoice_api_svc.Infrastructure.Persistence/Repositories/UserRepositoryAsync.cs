using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Domain.Entities;
using invoice_api_svc.Infrastructure.Persistence.Contexts;
using invoice_api_svc.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System;
using invoice_api_svc.Application.Features.Partners.Queries.GetAllPartners;
using invoice_api_svc.Application.Wrappers;

namespace invoice_api_svc.Infrastructure.Persistence.Repositories
{
    public class UserRepositoryAsync : GenericRepositoryAsync<User>, IUserRepositoryAsync
    {
        private readonly DbSet<User> _user;

        public UserRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _user = dbContext.Set<User>();
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await _user.AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<PagedResponse<IReadOnlyList<User>>> GetPagedResponseAsync(GetAllPartnerFilter filter)
        {
            var query = _user.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(u => u.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.CompanyName))
            {
                query = query.Where(u => u.CompanyName.ToLower().Contains(filter.CompanyName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                query = query.Where(u => u.Email.Contains(filter.Email));
            }

            if (!string.IsNullOrWhiteSpace(filter.Phone))
            {
                query = query.Where(u => u.Phone.Contains(filter.Phone));
            }

            if (!string.IsNullOrWhiteSpace(filter.LicenseKey))
            {
                query = query.Where(u => u.LicenseKey.Contains(filter.LicenseKey));
            }

            if (filter.Status.HasValue)
            {
                query = query.Where(u => u.Status == filter.Status.Value);
            }

            var totalCount = await query.CountAsync();

            var users = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .AsNoTracking()
                .ToListAsync();

            return new PagedResponse<IReadOnlyList<User>>(users, filter.PageNumber, filter.PageSize, totalCount);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _user.FirstOrDefaultAsync(user => user.Email == email);
        }
    }
}
