using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using NexKoala.Framework.Core.Paging;
using NexKoala.Framework.Core.Specifications;
using NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Get.v1;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Specifications;

public class ClassificationMappingListFilterSpec : EntitiesByPaginationFilterSpec<ClassificationMapping, ClassificationMappingResponse>
{
    public ClassificationMappingListFilterSpec(int pageNumber, int pageSize, Guid? userId = null)
        : base(new PaginationFilter { PageNumber = pageNumber, PageSize = pageSize })
    {
        if (userId.HasValue)
        {
            Query.Where(um => um.Classification.UserId == userId && !um.IsDeleted);
        }
    }
}
