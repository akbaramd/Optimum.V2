using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Optimum.Api.Contracts;

public interface IEndpointConfigure
{
    Task MapGet<TRequest, TResponse>(string pattern, string[]? roles = null, string[]? policies = null)
        where TRequest : IApiRequest<TResponse>;

    Task MapGet(string pattern, RequestDelegate action, string[]? roles = null,
        string[]? policies = null);
        

    Task MapPost<TRequest, TResponse>(string pattern, string[]? roles = null, string[]? policies = null)
        where TRequest : IApiRequest<TResponse>;

    Task MapDelete<TRequest, TResponse>(string pattern, string[]? roles = null, string[]? policies = null)
        where TRequest : IApiRequest<TResponse>;

    Task MapPut<TRequest, TResponse>(string pattern, string[]? roles = null, string[]? policies = null)
        where TRequest : IApiRequest<TResponse>;
}