using System.Reflection.Metadata;
using Optimum.Api.Contracts;

namespace Optimum.Api.Handlers;

public interface IApiRequestHandler<TRequest,TResponse> where TRequest: IApiRequest<TResponse>
{
    Task<TResponse> HandleAsync(RequestContext<TRequest> context , CancellationToken cancellationToken = default);
}

