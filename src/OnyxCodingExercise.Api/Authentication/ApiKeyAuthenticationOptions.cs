using Microsoft.AspNetCore.Authentication;

namespace OnyxCodingExercise.Api.Authentication;

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public string? ApiKey { get; set; }
    public string HeaderName { get; set; } = ApiKeyAuthenticationDefaults.HeaderName;

    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(ApiKey))
        {
            throw new InvalidOperationException("The API key must be provided.");
        }

        if (string.IsNullOrWhiteSpace(HeaderName))
        {
            throw new InvalidOperationException("The header name must be provided.");
        }
    }
}
