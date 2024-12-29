using invoice_api_svc.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace invoice_api_svc.Domain.Entities
{
    public class User : AuditableBaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; }

        [StringLength(20)]
        public string Tin { get; set; }

        [StringLength(20)]
        public string SchemeID { get; set; }

        [StringLength(20)]
        public string RegistrationNumber { get; set; }

        [StringLength(20)]
        public string SSTRegistrationNumber { get; set; }

        [StringLength(30)]
        public string TourismTaxRegistrationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string MSICCode { get; set; }

        [StringLength(200)]
        public string BusinessActivityDescription { get; set; }

        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(10)]
        public string CountryCode { get; set; }

        [Required]
        [StringLength(50)]
        public string LicenseKey { get; set; }

        public bool Status { get; set; }

        public int SubmissionCount { get; set; }

        public int MaxSubmissions { get; set; }

    }
}
