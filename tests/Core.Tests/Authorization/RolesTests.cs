using NexKoala.WebApi.Shared.Authorization;
using Xunit;

namespace NexKoala.Framework.Core.Tests.Authorization;

public class RolesTests
{
    [Theory]
    [InlineData(Roles.Admin, true)]
    [InlineData(Roles.Basic, true)]
    [InlineData("Other", false)]
    public void IsDefault_ReturnsExpectedValue(string role, bool expected)
    {
        var result = Roles.IsDefault(role);
        Assert.Equal(expected, result);
    }
}
