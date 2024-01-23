using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using OnyxCodingExercise.Api.Authentication;
using System.Text.Encodings.Web;

namespace OnyxCodingExercise.Api.Tests.Authentication;

public class ApiKeyAuthenticationHandlerTests : IAsyncLifetime
{
    private readonly string _apiKey = Guid.NewGuid().ToString();
    private readonly string _headerName = ApiKeyAuthenticationDefaults.HeaderName;

    private readonly ApiKeyAuthenticationOptions _options;
    private readonly ApiKeyAuthenticationHandler _handler;

    private readonly DefaultHttpContext _httpContext = new();

    public ApiKeyAuthenticationHandlerTests()
    {
        _options = new()
        {
            ApiKey = _apiKey,
            HeaderName = ApiKeyAuthenticationDefaults.HeaderName,
        };

        var optionsMonitor = Substitute.For<IOptionsMonitor<ApiKeyAuthenticationOptions>>();
        optionsMonitor.CurrentValue.Returns(_options);
        optionsMonitor.Get(Arg.Is(ApiKeyAuthenticationDefaults.AuthenticationScheme)).Returns(_options);

        _handler = new(optionsMonitor, NullLoggerFactory.Instance, UrlEncoder.Default);
    }

    public async Task InitializeAsync()
    {
        await _handler.InitializeAsync(
            new AuthenticationScheme(ApiKeyAuthenticationDefaults.AuthenticationScheme, null, typeof(ApiKeyAuthenticationHandler)),
            _httpContext);
    }

    [Fact]
    public async Task GivenARequestWithoutApiKey_WhenAuthenticating_ShouldFail()
    {
        var result = await _handler.AuthenticateAsync();

        result.Succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task GivenARequestWithInvalidApiKey_WhenAuthenticating_ShouldFail()
    {
        _httpContext.Request.Headers.Append(_headerName, Guid.NewGuid().ToString());

        var result = await _handler.AuthenticateAsync();

        result.Succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task GivenARequestWithValidApiKey_WhenAuthenticating_ShouldSucceed()
    {
        _httpContext.Request.Headers.Append(_headerName, _apiKey);

        var result = await _handler.AuthenticateAsync();

        result.Succeeded.Should().BeTrue();
    }

    [Fact]
    public async Task GivenARequestWithValidApiKeyWithInvalidCaseVariation_ShouldFail()
    {
        _httpContext.Request.Headers.Append(_headerName, _apiKey.ToUpper());

        var result = await _handler.AuthenticateAsync();

        result.Succeeded.Should().BeFalse();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}
