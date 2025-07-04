﻿using System.Security.Claims;

namespace NexKoala.Framework.Core.Identity.Users.Abstractions;
public interface ICurrentUser
{
    string? Name { get; }

    Guid GetUserId();

    string? GetUserEmail();

    string? GetTenant();

    bool IsAuthenticated();

    bool IsInRole(string role);

    IEnumerable<Claim>? GetUserClaims();
}
