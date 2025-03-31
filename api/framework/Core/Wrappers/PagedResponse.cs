using System;
using System.Collections.Generic;
using System.Linq;
using Mapster;

namespace NexKoala.Framework.Core.Wrappers
{
    public class PagedResponse<T> : Response<IReadOnlyList<T>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalCount = 0)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data.ToList();
            Message = null;
            Succeeded = true;
            Errors = null;
        }

        public PagedResponse<TR> MapTo<TR>(Func<T, TR> map)
        {
            return new PagedResponse<TR>(Data.Select(map).ToList(), PageNumber, PageSize, TotalCount);
        }

        public PagedResponse<TR> MapTo<TR>()
        {
            return new PagedResponse<TR>(Data.Adapt<IReadOnlyList<TR>>(), PageNumber, PageSize, TotalCount);
        }
    }
}
