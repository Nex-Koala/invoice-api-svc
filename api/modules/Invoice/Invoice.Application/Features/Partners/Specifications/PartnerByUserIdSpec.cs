using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Specifications;
public class PartnerByUserIdSpec : Specification<Partner, PartnerResponse>
{
    public PartnerByUserIdSpec(string userId)
    {
        Query.Where(p => p.UserId.ToLower() == userId.ToLower());
    }
}
