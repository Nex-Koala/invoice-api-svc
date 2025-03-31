using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using System.Xml;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using iText.Html2pdf;
using QRCoder;
using Microsoft.AspNetCore.Hosting;

namespace NexKoala.WebApi.Invoice.Infrastructure.Services;
public class InvoiceService(IWebHostEnvironment env) : IInvoiceService
{
    public byte[] GenerateInvoice(string xmlContent, string xsltTemplatePath)
    {
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltTemplatePath);

        string transformedHtml;
        using (StringWriter stringWriter = new StringWriter())
        {
            XmlWriterSettings writerSettings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                ConformanceLevel = ConformanceLevel.Auto,
                Indent = true
            };

            using (XmlReader xmlReader = XmlReader.Create(new StringReader(xmlContent)))
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, writerSettings))
            {
                xslt.Transform(xmlReader, xmlWriter);
            }
            transformedHtml = stringWriter.ToString();
        }

        byte[] pdfBytes;
        using (MemoryStream pdfStream = new MemoryStream())
        {
            HtmlConverter.ConvertToPdf(transformedHtml, pdfStream);
            pdfBytes = pdfStream.ToArray();
            return pdfBytes;
        }
    }

    public string GenerateQRCode(string data)
    {
        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        {
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q))
            {
                using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
                {
                    byte[] qrCodeBytes = qrCode.GetGraphic(20);
                    return "data:image/png;base64," + Convert.ToBase64String(qrCodeBytes);
                }
            }
        }
    }
}
