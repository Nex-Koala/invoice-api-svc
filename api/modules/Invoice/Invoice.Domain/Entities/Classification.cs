using NexKoala.Framework.Core.Domain;
using NexKoala.Framework.Core.Domain.Contracts;

namespace NexKoala.WebApi.Invoice.Domain.Entities;

public class Classification : AuditableEntity, IAggregateRoot
{
    public Guid UserId { get; set; }
    public string Code { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public bool IsDeleted { get; set; } = false;
    public ICollection<ClassificationMapping> ClassificationMappings { get; set; } = new List<ClassificationMapping>();

    public static Classification Create(Guid userId, string code, string? description)
    {
        var classification = new Classification();
        classification.UserId = userId;
        classification.Code = code;
        classification.Description = description;

        return classification;
    }

    public Classification Update(string? code, string? description)
    {
        if (code is not null && Code?.Equals(code, StringComparison.OrdinalIgnoreCase) is not true) Code = code;
        if (description is not null && Description?.Equals(description, StringComparison.OrdinalIgnoreCase) is not true) Description = description;

        return this;
    }
}
