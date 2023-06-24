using Optimum.WebApi;
using Optimum.WebApi.Contracts;
using Optimum.WebApi.Handlers;

namespace Optimum.Example.Service.Requests;

public class ServiceHealthRequest : IRequest<ServiceHealthResponse>
{
    public string ServiceName { get; set; }
}

public class ServiceHealthRequestHandler : IRequestHandler<ServiceHealthRequest,ServiceHealthResponse>
{
    public Task<ServiceHealthResponse> HandleAsync(RequestContext<ServiceHealthRequest> context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new ServiceHealthResponse()
        {
            Status = context.PathParams.GetInt("Status") ?? 0,
            ServiceName = context.Body.ServiceName??"None"
        });
    }
}


public class ServiceHealthResponse
{
    public int Status { get; set; }
    public string ServiceName { get; set; }
}