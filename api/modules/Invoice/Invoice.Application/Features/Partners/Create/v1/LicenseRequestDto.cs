using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Create.v1;
public class LicenseRequestDto
{
    public DateTimeOffset ExpiryDate { get; set; }
    public int MaxSubmissions { get; set; }
    public bool IsRevoked { get; set; }
    public bool GenerateNewKey { get; set; }
}
