using Microsoft.AspNetCore.Identity;

namespace NexKoala.Framework.Infrastructure.Identity.Roles;
public class Role : IdentityRole
{
    public string? Description { get; set; }

    public Role(string name, string? description = null)
        : base(name)
    {
        Description = description;
        NormalizedName = name.ToUpperInvariant();
    }
}
