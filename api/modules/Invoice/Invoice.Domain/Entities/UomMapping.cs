using NexKoala.Framework.Core;
using NexKoala.Framework.Core.Domain;
using NexKoala.Framework.Core.Domain.Contracts;

namespace NexKoala.WebApi.Invoice.Domain.Entities;

public class UomMapping : AuditableEntity, IAggregateRoot
{
    public string LhdnUomCode { get; set; } = default!;
    public Guid UomId { get; set; }
    public Uom Uom { get; set; } = default!;
    public bool IsDeleted { get; set; } = false;

    public static UomMapping Create(string code, Guid uomId)
    {
        return new UomMapping
        {
            LhdnUomCode = code,
            UomId = uomId
        };
    }

    public UomMapping Update(string? code, Guid? uomId)
    {
        if (!string.IsNullOrEmpty(code) && !LhdnUomCode.Equals(code, StringComparison.OrdinalIgnoreCase))
            LhdnUomCode = code;

        if (uomId.HasValue && UomId != uomId.Value)
            UomId = uomId.Value;

        return this;
    }
}
