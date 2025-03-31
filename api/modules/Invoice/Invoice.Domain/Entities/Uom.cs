using System.Diagnostics;
using System.Xml.Linq;
using NexKoala.Framework.Core.Domain;
using NexKoala.Framework.Core.Domain.Contracts;

namespace NexKoala.WebApi.Invoice.Domain.Entities;

public class Uom : AuditableEntity, IAggregateRoot
{
    public Guid UserId { get; set; } // Reference to Seller (optional if needed)
    public string Code { get; set; } = default!; // Unique Code for Seller UOM
    public string? Description { get; set; } = default!; // Description of the UOM
    public bool IsDeleted { get; set; } = false;
    // Navigation Property for Mappings
    public ICollection<UomMapping> UomMappings { get; set; } = new List<UomMapping>();

    public static Uom Create(Guid userId, string code, string? description)
    {
        var uom = new Uom();
        uom.UserId = userId;
        uom.Code = code;
        uom.Description = description;

        return uom;
    }

    public Uom Update(string? code, string? description)
    {
        if (code is not null && Code?.Equals(code, StringComparison.OrdinalIgnoreCase) is not true) Code = code;
        if (description is not null && Description?.Equals(description, StringComparison.OrdinalIgnoreCase) is not true) Description = description;

        return this;
    }
}
