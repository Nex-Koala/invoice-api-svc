using invoice_api_svc.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Partners.Queries.GetAllPartners
{
    public class GetAllPartnerFilter : RequestParameter
    {
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? LicenseKey { get; set; }
        public bool? Status { get; set; }
    }
}
