namespace NexKoala.Framework.Core.Identity.Tokens.Models;
public record TokenResponse(string Id, string UserName, string Email, List<string> Roles, string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);
