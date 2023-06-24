using System.Reflection.Metadata;
using Optimum.WebApi.Contracts;

namespace Optimum.WebApi.Handlers;

public interface IRequestHandler<TRequest,TResponse> where TRequest: IRequest<TResponse> where TResponse : class
{
    Task<TResponse> HandleAsync(RequestContext<TRequest> context , CancellationToken cancellationToken = default);
}

