using OnyxCodingExercise.Api.Authentication;

namespace OnyxCodingExercise.Api.Tests.Authentication;

public class ApiKeyAuthenticationOptionsTests
{
    [Fact]
    public void GivenOptions_WithoutApiKey_WhenValidating_ShouldThrowException()
    {
        var sut = new ApiKeyAuthenticationOptions
        {
            ApiKey = string.Empty,
            HeaderName = "X-Api-Key",
        };

        var action = () => sut.Validate();

        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenOptions_WithoutHeaderName_WhenValidating_ShouldThrowException()
    {
        var sut = new ApiKeyAuthenticationOptions
        {
            ApiKey = "123",
            HeaderName = string.Empty,
        };

        var action = () => sut.Validate();

        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenOptions_WithApiKeyAndHeaderName_WhenValidating_ShouldNotThrowException()
    {
        var sut = new ApiKeyAuthenticationOptions
        {
            ApiKey = "123",
            HeaderName = "X-Api-Key",
        };

        var action = () => sut.Validate();

        action.Should().NotThrow<InvalidOperationException>();
    }

    [Fact]
    public void GivenDefaultOptions_ValuesShouldBeInitialised()
    {
        var sut = new ApiKeyAuthenticationOptions();

        sut.ApiKey.Should().BeNull();
        sut.HeaderName.Should().Be(ApiKeyAuthenticationDefaults.HeaderName);
    }
}
