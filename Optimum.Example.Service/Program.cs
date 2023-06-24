using Optimum;
using Optimum.WebApi;
using Optimum.CQRS;
using Optimum.Example.Service.Requests;

OptimumApplication
    .CreateServiceBuilder("example-service", "Example Service")
    .AddWebApi()
    .AddCommandHandlers()
    .AddQueryHandlers()
    .AddEventHandlers()
    .AddInMemoryCommandDispatchers()
    .AddInMemoryQueryDispatchers()
    .AddInMemoryEventDispatchers()
    .Build()
    .UseEndpoints(endpoints => endpoints
        .MapGet<ServiceHealthRequest, ServiceHealthResponse>("/health")
        .MapPost<ServiceHealthRequest, ServiceHealthResponse>("/health-post/{status}"))
    .Run();