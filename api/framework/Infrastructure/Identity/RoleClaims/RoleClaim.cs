using Microsoft.AspNetCore.Identity;

namespace NexKoala.Framework.Infrastructure.Identity.RoleClaims;
public class RoleClaim : IdentityRoleClaim<string>
{
    public string? CreatedBy { get; init; }
    public DateTimeOffset CreatedOn { get; init; }
}
