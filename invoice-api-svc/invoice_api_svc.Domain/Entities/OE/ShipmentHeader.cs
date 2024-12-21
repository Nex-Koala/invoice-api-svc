using System;
using System.Collections.Generic;

namespace invoice_api_svc.Domain.Entities.OE
{
    public class ShipmentHeader
    {
        public int ShipmentId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string CustomerName { get; set; }
        public ICollection<ShipmentDetail> ShipmentDetails { get; set; }
    }
}
