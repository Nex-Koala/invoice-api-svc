using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Classifications.Specifications;
internal class ClassificationMappingWithClassification: Specification<ClassificationMapping, ClassificationMapping>
{
    public ClassificationMappingWithClassification(Guid userId, List<string> code)
    {
        Query.Where(um =>
            um.Classification.UserId == userId &&
            code.Contains(um.Classification.Code) &&
            !um.IsDeleted);
    }
}
