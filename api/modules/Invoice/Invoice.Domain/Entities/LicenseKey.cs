using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexKoala.Framework.Core.Domain;
using NexKoala.Framework.Core.Domain.Contracts;

namespace NexKoala.WebApi.Invoice.Domain.Entities;

public class LicenseKey : AuditableEntity, IAggregateRoot
{
    public Guid Key { get; set; } = Guid.NewGuid(); 
    public Guid PartnerId { get; set; }
    public Partner Partner { get; set; }
    public int MaxSubmissions { get; set; }
    public int SubmissionCount { get; set; }
    public DateTimeOffset ExpiryDate { get; set; } = DateTime.UtcNow.AddYears(1);
    public bool IsRevoked { get; set; }
    public LicenseStatus Status => CalculateStatus();

    public LicenseKey()
    {
        Key = Guid.NewGuid();
        MaxSubmissions = 1;
        ExpiryDate = DateTime.UtcNow.AddYears(1);
        IsRevoked = false;
        SubmissionCount = 0;
    }

    private LicenseStatus CalculateStatus()
    {
        if (IsRevoked) return LicenseStatus.Revoked;
        if (DateTimeOffset.UtcNow > ExpiryDate) return LicenseStatus.Expired;
        if (SubmissionCount >= MaxSubmissions) return LicenseStatus.UsedUp;
        return LicenseStatus.Active;
    }

    public void GenerateNewKey(int maxSubmissions, DateTimeOffset expiryDate)
    {
        Key = Guid.NewGuid();
        MaxSubmissions = maxSubmissions;
        ExpiryDate = expiryDate;
        IsRevoked = false;
        SubmissionCount = 0;
    }
}

public enum LicenseStatus
{
    Active,
    Expired,
    UsedUp,
    Revoked
}
