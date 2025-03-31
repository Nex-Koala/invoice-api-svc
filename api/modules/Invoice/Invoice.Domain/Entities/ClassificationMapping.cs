using NexKoala.Framework.Core.Domain;
using NexKoala.Framework.Core.Domain.Contracts;

namespace NexKoala.WebApi.Invoice.Domain.Entities;

public class ClassificationMapping : AuditableEntity, IAggregateRoot
{
    public string LhdnClassificationCode { get; set; } = default!;
    public Guid ClassificationId { get; set; }
    public Classification Classification { get; set; } = default!;
    public bool IsDeleted { get; set; } = false;

    public static ClassificationMapping Create(string code, Guid classificationId)
    {
        return new ClassificationMapping
        {
            LhdnClassificationCode = code,
            ClassificationId = classificationId
        };
    }

    public ClassificationMapping Update(string? code, Guid? classificationId)
    {
        if (!string.IsNullOrEmpty(code) && !LhdnClassificationCode.Equals(code, StringComparison.OrdinalIgnoreCase))
            LhdnClassificationCode = code;

        if (classificationId.HasValue && ClassificationId != classificationId.Value)
            ClassificationId = classificationId.Value;

        return this;
    }
}

