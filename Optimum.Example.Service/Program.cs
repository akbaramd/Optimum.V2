using Optimum;
using Optimum.WebApi;
using Optimum.WebApi.CQRS;
using Optimum.CQRS;
using Optimum.Example.Service.Commands;
using Optimum.Example.Service.Requests;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddSerilog();

builder.Services.AddOptimum("example-service", "Example Service", configure =>
{
    configure.AddWebApi();
    configure.AddCommandHandlers();
    configure.AddEventHandlers();
    configure.AddQueryHandlers();
    configure.AddInMemoryCommandDispatchers();
    configure.AddInMemoryQueryDispatchers();
    configure.AddInMemoryEventDispatchers();
});

var app = builder.Build();

app.UseOptimum(configure =>
{
    configure.UseEndpoints(async endpoints => endpoints
        .MapGet<ServiceHealthRequest, ServiceHealthResponse>("/health")
        .MapPost<ServiceHealthRequest, ServiceHealthResponse>("/health-post/{status}"));
});

app.Run();