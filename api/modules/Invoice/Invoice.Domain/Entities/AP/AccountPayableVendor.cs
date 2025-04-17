namespace NexKoala.WebApi.Invoice.Domain.Entities.AP;

public class AccountPayableVendor
{
    public string VENDORID { get; set; } // Vendor ID (SAGE: APVEN.VENDORID)
    public string BRN { get; set; } // Business Registration Number (SAGE: APVEN.BRN)
}
