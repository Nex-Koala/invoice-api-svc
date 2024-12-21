using System;
using System.Collections.Generic;

namespace invoice_api_svc.Domain.Entities.OE
{
    public class OrderCreditDebitHeader
    {
        public int CreditDebitId { get; set; }
        public DateTime DocumentDate { get; set; }
        public string Description { get; set; }
        public ICollection<OrderCreditDebitDetail> CreditDebitDetails { get; set; }
    }
}
