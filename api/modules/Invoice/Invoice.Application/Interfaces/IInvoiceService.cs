using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Interfaces;
public interface IInvoiceService
{
    byte[] GenerateInvoice(string xmlContent, string xsltTemplatePath);

    string GenerateQRCode(string data);
}
