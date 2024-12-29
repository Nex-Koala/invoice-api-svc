using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace invoice_api_svc.Application.Features.Partners.Queries.GetAllPartners
{
    public class PartnersViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string CompanyName { get; set; }

        public string Tin { get; set; }

        public string SchemeID { get; set; }

        public string RegistrationNumber { get; set; }

        public string SSTRegistrationNumber { get; set; }

        public string TourismTaxRegistrationNumber { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string MSICCode { get; set; }

        public string BusinessActivityDescription { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string CountryCode { get; set; }

        public string LicenseKey { get; set; }

        public bool Status { get; set; }

        public int SubmissionCount { get; set; }

        public int MaxSubmissions { get; set; }
    }
}
