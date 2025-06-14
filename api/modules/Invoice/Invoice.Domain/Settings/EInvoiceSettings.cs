using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Domain.Settings;

public class EInvoiceSettings
{
    public string ApiBaseUrl { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string OnBehalfOf { get; set; }
    public string TemplatePath { get; set; }
    public string AdminTin { get; set; }
    public string MyInvoiceBaseUrl { get; set; }
}
