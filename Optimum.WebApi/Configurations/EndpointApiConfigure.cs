using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Optimum.Extensions;
using Optimum.WebApi.Contracts;
using Optimum.WebApi.Handlers;

namespace Optimum.WebApi.Configurations;

public class EndpointApiConfigure : IEndpointConfigure
{
    private readonly IEndpointRouteBuilder _routeBuilder; 
    private readonly IServiceProvider  _serviceProvider;

    public EndpointApiConfigure(IEndpointRouteBuilder routeBuilder, IServiceProvider serviceProvider)
    {
        _routeBuilder = routeBuilder;
        _serviceProvider = serviceProvider;
    }

    public IEndpointConfigure MapGet<TRequest, TResponse>(string pattern , string[]? roles = null, string[]? policies = null) where TRequest : IRequest<TResponse> where TResponse : class
    {
        var handler = GetRequestHandlerService<TRequest, TResponse>();
        MapGet(pattern, async context =>
        {
            var requestContext = await BuildReqeustContext<TRequest>(context);
            var response = await handler.HandleAsync(requestContext);
            await context.Response.WriteAsJsonAsync(response);
        });
        return this;
    }

    public IEndpointConfigure MapGet(string pattern, RequestDelegate action, string[]? roles = null, string[]? policies = null)
    {
        _routeBuilder.MapGet(pattern, action);
        return this;
    }

    public IEndpointConfigure MapPost<TRequest, TResponse>(string pattern , string[]? roles = null, string[]? policies = null) where TRequest : IRequest<TResponse> where TResponse : class
    {
        var handler = GetRequestHandlerService<TRequest, TResponse>();
        _routeBuilder.MapPost(pattern, async context =>
        {
            var requestContext = await BuildReqeustContext<TRequest>(context);
            var response = await handler.HandleAsync(requestContext);
            await context.Response.WriteAsJsonAsync(response);
        });
        return this;
    }

    public IEndpointConfigure MapDelete<TRequest, TResponse>(string pattern , string[]? roles = null, string[]? policies = null) where TRequest : IRequest<TResponse> where TResponse : class
    {
        var handler = GetRequestHandlerService<TRequest, TResponse>();
        _routeBuilder.MapDelete(pattern, async context =>
        {
            var requestContext = await BuildReqeustContext<TRequest>(context);
            var response = await handler.HandleAsync(requestContext);
            await context.Response.WriteAsJsonAsync(response);
        });
        return this;
    }

    public  IEndpointConfigure MapPut<TRequest, TResponse>(string pattern , string[]? roles = null, string[]? policies = null) where TRequest : IRequest<TResponse> where TResponse : class
    {
        var handler = GetRequestHandlerService<TRequest, TResponse>();
        _routeBuilder.MapPut(pattern, async context =>
        {
            var requestContext = await BuildReqeustContext<TRequest>(context);
            var response = await handler.HandleAsync(requestContext);
            await context.Response.WriteAsJsonAsync(response);
        });
        return this;
    }

    private IRequestHandler<TRequest, TResponse> GetRequestHandlerService<TRequest, TResponse>()
        where TRequest : IRequest<TResponse> where TResponse : class
    {
        var tResponse = typeof(TResponse);
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var implementationType = assemblies
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => t.GetInterfaces()
                                     .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>))
                                 && t.GetInterfaces()
                                     .Any(i => i.GetGenericArguments().Any(arg => arg == tResponse)));

        var handler = _serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        return handler;
    }
    private async Task<RequestContext<TRequest>> BuildReqeustContext<TRequest>(HttpContext context) 
    {
        
        var requestBodyString = await new StreamReader(context.Request.Body).ReadToEndAsync();
        var requestBody = Activator.CreateInstance<TRequest>();

        if (!string.IsNullOrWhiteSpace(requestBodyString))
        {
            if (requestBodyString.IsJson())
            {
                requestBody = JsonConvert.DeserializeObject<TRequest>(requestBodyString) ?? throw new InvalidOperationException();
            }
            else
            {
                throw new BadHttpRequestException("your request body in not json format");
            }
        }

        var routeValues = new RequestPathParams();
        foreach (var routeValue in context.Request.RouteValues)
        {
            routeValues[routeValue.Key] = routeValue.Value?.ToString()??"";
        }
        
        var queryStrings = new RequestQueryStrings();
        foreach (var query in context.Request.Query)
        {
            queryStrings[query.Key]=query.Value.ToString();
        }
        
        return new RequestContext<TRequest>()
        {
            Body = requestBody,
            PathParams = routeValues,
            QueryStrings = queryStrings,
        };
    }
}