using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Specifications;
public class PartnerByEmailSpec : Specification<Partner, PartnerResponse>
{
    public PartnerByEmailSpec(string email)
    {
        Query.Where(p => p.Email.ToLower() == email.ToLower());
    }
}
