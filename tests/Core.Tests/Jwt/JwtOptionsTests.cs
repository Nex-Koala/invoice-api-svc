using System.ComponentModel.DataAnnotations;
using System.Linq;
using NexKoala.Framework.Core.Auth.Jwt;
using Xunit;

namespace NexKoala.Framework.Core.Tests.Jwt;

public class JwtOptionsTests
{
    [Fact]
    public void Validate_ReturnsErrorWhenKeyIsEmpty()
    {
        var options = new JwtOptions { Key = string.Empty };
        var result = options.Validate(new ValidationContext(options)).ToList();
        Assert.Single(result);
        Assert.Equal("Key", result[0].MemberNames.Single());
    }

    [Fact]
    public void Validate_ReturnsNoErrorsWhenKeyProvided()
    {
        var options = new JwtOptions { Key = "secret" };
        var result = options.Validate(new ValidationContext(options)).ToList();
        Assert.Empty(result);
    }
}
