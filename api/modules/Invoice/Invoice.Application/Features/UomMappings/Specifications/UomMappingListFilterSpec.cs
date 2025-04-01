using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using NexKoala.Framework.Core.Paging;
using NexKoala.Framework.Core.Specifications;
using NexKoala.WebApi.Invoice.Application.Features.UomMappings.Get.v1;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.UomMappings.Specifications;

public class UomMappingListFilterSpec : EntitiesByPaginationFilterSpec<UomMapping, UomMappingResponse>
{
    public UomMappingListFilterSpec(int pageNumber, int pageSize, Guid? userId = null)
        : base(new PaginationFilter { PageNumber = pageNumber, PageSize = pageSize })
    {
        if (userId.HasValue)
        {
            Query.Where(um => um.Uom.UserId == userId && !um.IsDeleted);
        }
    }
}
