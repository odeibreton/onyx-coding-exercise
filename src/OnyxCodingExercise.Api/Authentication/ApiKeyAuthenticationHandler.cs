using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace OnyxCodingExercise.Api.Authentication;

public class ApiKeyAuthenticationHandler(
    IOptionsMonitor<ApiKeyAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<ApiKeyAuthenticationOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var apiKey = Request.Headers[Options.HeaderName];

        if (apiKey != Options.ApiKey)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API key provided."));
        }

        var principal = new ClaimsPrincipal(new ClaimsIdentity(Scheme.Name));
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
