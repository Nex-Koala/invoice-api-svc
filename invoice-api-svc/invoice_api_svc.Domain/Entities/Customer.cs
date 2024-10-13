using System.ComponentModel.DataAnnotations;

namespace invoice_api_svc.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } // "Buyer's Name"

        [StringLength(20)]
        public string Tin { get; set; } // Buyer's TIN

        [StringLength(20)]
        public string Brn { get; set; } // Buyer's BRN

        [StringLength(100)]
        public string Address { get; set; } // Buyer's Address

        [StringLength(50)]
        public string City { get; set; } // "Kuala Lumpur"

        [StringLength(10)]
        public string PostalCode { get; set; } // "50480"

        [StringLength(10)]
        public string CountryCode { get; set; } // "MYS"
    }

}
