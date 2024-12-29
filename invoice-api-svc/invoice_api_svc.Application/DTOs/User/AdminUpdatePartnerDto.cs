using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.User
{
    public class AdminUpdatePartnerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LicenseKey { get; set; }
        public bool Status { get; set; }
        public int MaxSubmissions { get; set; }
    }
}
