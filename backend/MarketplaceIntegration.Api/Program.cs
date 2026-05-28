using MarketplaceIntegration.Api.Application.Abstractions;
using MarketplaceIntegration.Api.Application.Services;
using MarketplaceIntegration.Api.Infrastructure.Connectors;
using MarketplaceIntegration.Api.Infrastructure.Persistence;
using MarketplaceIntegration.Api.Infrastructure.Repositories;
using MarketplaceIntegration.Api.Infrastructure.Sync;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins(builder.Configuration.GetValue<string>("Frontend:Url") ?? "http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var provider = builder.Configuration.GetValue<string>("Database:Provider") ?? "InMemory";

builder.Services.AddDbContext<MarketplaceDbContext>(options =>
{
    if (provider.Equals("Postgres", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(connectionString))
    {
        options.UseNpgsql(connectionString);
        return;
    }

    options.UseInMemoryDatabase("MarketplaceMvpDb");
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IChannelRepository, ChannelRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ChannelService>();

builder.Services.AddSingleton<ISyncJobQueue, InMemorySyncJobQueue>();
builder.Services.AddHostedService<SyncWorker>();

builder.Services.AddScoped<IMarketplaceConnector, AmazonMarketplaceConnector>();
builder.Services.AddScoped<IMarketplaceConnector, EbayMarketplaceConnector>();

var app = builder.Build();

app.UseExceptionHandler(handler =>
{
    handler.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        var result = Results.Problem(
            detail: exception?.Message,
            statusCode: StatusCodes.Status500InternalServerError,
            title: "Unexpected server error");

        await result.ExecuteAsync(context);
    });
});

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("Frontend");

app.MapControllers();
app.MapHealthChecks("/health");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MarketplaceDbContext>();
    await db.Database.EnsureCreatedAsync();
}

app.Run();
