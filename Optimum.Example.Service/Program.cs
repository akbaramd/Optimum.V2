using Optimum;
using Optimum.Api;
using Optimum.CQRS;
using Optimum.Example.Service.Requests;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddSerilog();

builder.Services.AddOptimum("example-service", "Example Service", configure =>
{
    configure.AddCQRS();
    configure.AddApiEndpoints();
});

var app = builder.Build();

app.UseOptimum(configure =>
{
    configure.UseApiEndpoints(async endpoints =>
    {
        await endpoints.MapGet<ServiceHealthRequest, ServiceHealthResponse>("/health");
        await endpoints.MapPost<ServiceHealthRequest, ServiceHealthResponse>("/health-post/{status}");
    });
});

app.Run();