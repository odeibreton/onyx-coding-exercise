using OnyxCodingExercise.Infrastructure;
using System.Text.Json.Serialization;
using System.Text.Json;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHealthChecks("/").AllowAnonymous();

app.MapControllers();

app.Run();

public partial class Program { }
