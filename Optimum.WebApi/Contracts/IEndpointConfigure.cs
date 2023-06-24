using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Optimum.WebApi.Contracts;

public interface IEndpointConfigure
{
    IEndpointConfigure MapGet<TRequest, TResponse>(string pattern, string[]? roles = null, string[]? policies = null)
        where TRequest : IRequest<TResponse> where TResponse : class;

    IEndpointConfigure MapGet(string pattern, RequestDelegate action, string[]? roles = null,
        string[]? policies = null);
        

    IEndpointConfigure MapPost<TRequest, TResponse>(string pattern, string[]? roles = null, string[]? policies = null)
        where TRequest : IRequest<TResponse> where TResponse : class;

    IEndpointConfigure MapDelete<TRequest, TResponse>(string pattern, string[]? roles = null, string[]? policies = null)
        where TRequest : IRequest<TResponse> where TResponse : class;

    IEndpointConfigure MapPut<TRequest, TResponse>(string pattern, string[]? roles = null, string[]? policies = null)
        where TRequest : IRequest<TResponse> where TResponse : class;
}