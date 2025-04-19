using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.UomMappings.Specifications;
internal class UomMappingByUserIdAndCodeList : Specification<UomMapping, UomMapping>
{
    public UomMappingByUserIdAndCodeList(Guid userId, List<string> code)
    {
        Query.Where(um =>
            um.Uom.UserId == userId &&
            code.Contains(um.Uom.Code) &&
            !um.IsDeleted);
    }
}
