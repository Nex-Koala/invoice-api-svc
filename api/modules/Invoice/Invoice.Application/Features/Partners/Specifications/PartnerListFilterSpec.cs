using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ardalis.Specification;
using NexKoala.Framework.Core.Paging;
using NexKoala.Framework.Core.Specifications;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Specifications;

public class PartnerListFilterSpec : EntitiesByPaginationFilterSpec<Partner, PartnerResponse>
{
    public PartnerListFilterSpec(
        int pageNumber,
        int pageSize,
        string? name = null,
        string? companyName = null,
        string? email = null,
        string? phone = null,
        Guid? licenseKey = null,
        bool? status = null
    )
        : base(new PaginationFilter { PageNumber = pageNumber, PageSize = pageSize })
    {
        Query.Where(p =>
            (name == null || p.Name.ToLower().Contains(name.ToLower())) &&
            (companyName == null || p.CompanyName.ToLower().Contains(companyName.ToLower())) &&
            (email == null || p.Email.ToLower().Contains(email.ToLower())) &&
            (phone == null || p.Phone.ToLower().Contains(phone.ToLower())) &&
            (licenseKey == null || (p.LicenseKey != null && p.LicenseKey.Key == licenseKey.Value)) &&
            (status == null || p.Status == status)
        );
    }
}
