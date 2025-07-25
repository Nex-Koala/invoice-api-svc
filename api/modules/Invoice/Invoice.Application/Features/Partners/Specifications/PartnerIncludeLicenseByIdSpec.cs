using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Specifications;
public class PartnerIncludeLicenseByIdSpec : Specification<Partner, Partner>
{
    public PartnerIncludeLicenseByIdSpec(Guid id)
    {
        Query.Where(p => p.Id == id).Include(p => p.LicenseKey);
    }
}
