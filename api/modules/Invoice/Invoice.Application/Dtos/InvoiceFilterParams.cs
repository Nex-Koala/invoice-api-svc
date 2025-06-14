using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Dtos;

public class InvoiceFilterParams
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? InvoiceNumber { get; set; }
    public string? BuyerName { get; set; }
    public string? SupplierName { get; set; }
    public decimal? InvoiceDate { get; set; }
    public string? NoteNumber { get; set; }
}
