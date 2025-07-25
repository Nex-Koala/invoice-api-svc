using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;
public class LicenseKeyResponseDto
{
    public Guid Key { get; set; }
    public DateTimeOffset ExpiryDate { get; set; }
    public int MaxSubmissions { get; set; }
    public int SubmissionCount { get; set; }
    public bool IsRevoked { get; set; }
    public LicenseStatus Status { get; set; }
}
