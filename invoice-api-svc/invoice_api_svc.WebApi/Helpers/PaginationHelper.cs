using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace invoice_api_svc.WebApi.Helpers
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

    public class PaginatedResult<T>
    {
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public System.Collections.Generic.List<T> Data { get; set; }
    }
}
