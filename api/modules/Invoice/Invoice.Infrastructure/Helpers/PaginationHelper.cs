using Microsoft.EntityFrameworkCore;
using NexKoala.WebApi.Invoice.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Infrastructure.Helpers
{
    public static class PaginationHelper
    {
        public static async Task<PaginatedResult<T>> PaginateAsync<T>(IQueryable<T> query, int pageNumber, int pageSize) where T : class
        {
            var totalItems = await query.CountAsync();
            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return new PaginatedResult<T>
            {
                TotalItems = totalItems,
                Page = pageNumber,
                PageSize = pageSize,
                Data = data
            };
        }
    }
}
