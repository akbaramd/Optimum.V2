using Optimum.Api;
using Optimum.Api.Attributes;
using Optimum.Api.Contracts;
using Optimum.Api.Handlers;

namespace Optimum.Example.Service.Requests;

[GetRequest("/health")]
[PostRequest("/health-post")]
public class ServiceHealthRequest : IApiRequest<ServiceHealthResponse>
{
    public string ServiceName { get; set; }
}

public class ServiceHealthRequestHandler : IApiRequestHandler<ServiceHealthRequest,ServiceHealthResponse>
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