using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnyxCodingExercise.Domain;
using OnyxCodingExercise.Infrastructure;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnyxCodingExercise.Api.Tests.Fixtures;

public class WebAppFixture : IDisposable
{
    private readonly IServiceScope _serviceScope;

    public HttpClient HttpClient { get; }
    public IServiceProvider Services { get; }
    public Product[] ConfiguredProducts { get; }

    public JsonSerializerOptions JsonSerializerOptions { get; } = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    internal WebApplicationFactory<Program> WebApplicationFactory { get; }

    public WebAppFixture()
    {
        WebApplicationFactory = GetWebApplicationFactory(new WebApplicationFactory<Program>());

        HttpClient = WebApplicationFactory.CreateClient();

        _serviceScope = WebApplicationFactory.Services.CreateScope();
        Services = _serviceScope.ServiceProvider;

        ConfiguredProducts = Services.GetRequiredService<IOptions<MockProductRepositoryOptions>>().Value.Products;
    }

    internal virtual WebApplicationFactory<Program> GetWebApplicationFactory(WebApplicationFactory<Program> factory) =>
        factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration(configurationBuilder => configurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables());
        });

    public void Dispose()
    {
        _serviceScope.Dispose();
        HttpClient.Dispose();
        WebApplicationFactory.Dispose();

        GC.SuppressFinalize(this);
    }
}