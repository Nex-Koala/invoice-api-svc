using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using NexKoala.Framework.Core.Paging;
using NexKoala.Framework.Core.Specifications;
using NexKoala.WebApi.Invoice.Application.Features.Uoms.Get.v1;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Uoms.Specifications;

public class UomListFilterSpec : EntitiesByPaginationFilterSpec<Uom, UomResponse>
{
    public UomListFilterSpec(int pageNumber, int pageSize, Guid userId)
        : base(new PaginationFilter { PageNumber = pageNumber, PageSize = pageSize })
    {
        Query.Where(p => p.UserId == userId);
    }
}
