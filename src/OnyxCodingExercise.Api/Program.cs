using OnyxCodingExercise.Infrastructure;
using System.Text.Json.Serialization;
using System.Text.Json;
using OnyxCodingExercise.Api.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

// Repositories
builder.Services.AddTransient<IProductRepository, MockProductRepository>();

// Options and configuration
builder.Services.AddOptions();
builder.Services.Configure<MockProductRepositoryOptions>(builder.Configuration.GetSection("MockProductRepositoryOptions"));

// Authentication
builder.Services.AddAuthentication()
    .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationDefaults.AuthenticationScheme, options =>
    {
        builder.Configuration.Bind("ApiKeyAuthenticationOptions", options);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapHealthChecks("/").AllowAnonymous();

app.MapControllers().RequireAuthorization();

app.Run();

public partial class Program { }
