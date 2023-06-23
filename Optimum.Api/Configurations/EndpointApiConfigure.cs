using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Optimum.Api.Attributes;
using Optimum.Api.Contracts;
using Optimum.Api.Handlers;

namespace Optimum.Api.Configurations;

public class EndpointApiConfigure : IEndpointConfigure
{
    public  WebApplication WebApplication { get; set; }

    public EndpointApiConfigure(WebApplication webApplication)
    {
        WebApplication = webApplication;
    }


    public Task MapGet<TRequest, TResponse>(string pattern , string[]? roles = null, string[]? policies = null) where TRequest : IApiRequest<TResponse>
    {
        var handler = GetRequestHandlerService<TRequest, TResponse>();
        MapGet(pattern, async context =>
        {
            var requestContext = await BuildReqeustContext<TRequest>(context);
            var response = await handler.HandleAsync(requestContext);
            await context.Response.WriteAsJsonAsync(response);
        });
        return Task.CompletedTask;
    }

    public Task MapGet(string pattern, RequestDelegate action, string[]? roles = null, string[]? policies = null)
    {
        WebApplication.MapGet(pattern, action);
        return Task.CompletedTask;
    }

    public Task MapPost<TRequest, TResponse>(string pattern , string[]? roles = null, string[]? policies = null) where TRequest : IApiRequest<TResponse>
    {
        var handler = GetRequestHandlerService<TRequest, TResponse>();
        WebApplication.MapPost(pattern, async context =>
        {
            var requestContext = await BuildReqeustContext<TRequest>(context);
            var response = await handler.HandleAsync(requestContext);
            await context.Response.WriteAsJsonAsync(response);
        });
        return Task.CompletedTask;
    }

    public Task MapDelete<TRequest, TResponse>(string pattern , string[]? roles = null, string[]? policies = null) where TRequest : IApiRequest<TResponse>
    {
        var handler = GetRequestHandlerService<TRequest, TResponse>();
        WebApplication.MapDelete(pattern, async context =>
        {
            var requestContext = await BuildReqeustContext<TRequest>(context);
            var response = await handler.HandleAsync(requestContext);
            await context.Response.WriteAsJsonAsync(response);
        });
        return Task.CompletedTask;
    }

    public  Task MapPut<TRequest, TResponse>(string pattern , string[]? roles = null, string[]? policies = null) where TRequest : IApiRequest<TResponse>
    {
        var handler = GetRequestHandlerService<TRequest, TResponse>();
        WebApplication.MapPut(pattern, async context =>
        {
            var requestContext = await BuildReqeustContext<TRequest>(context);
            var response = await handler.HandleAsync(requestContext);
            await context.Response.WriteAsJsonAsync(response);
        });
        return Task.CompletedTask;
    }

    private IApiRequestHandler<TRequest, TResponse> GetRequestHandlerService<TRequest, TResponse>()
        where TRequest : IApiRequest<TResponse>
    {
        var tResponse = typeof(TResponse);
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var implementationType = assemblies
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => t.GetInterfaces()
                                     .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IApiRequest<>))
                                 && t.GetInterfaces()
                                     .Any(i => i.GetGenericArguments().Any(arg => arg == tResponse)));

        var handler = WebApplication.Services.GetRequiredService<IApiRequestHandler<TRequest, TResponse>>();
        return handler;
    }
    
    
    private Task<TRequest> GenerateModelFromQueryString<TRequest>(HttpContext context) 
    {
        
        var queryStringParams = context.Request.Query;

        // Create an instance of TRequest
        var requestModel = Activator.CreateInstance<TRequest>();

        // Get all properties of TRequest
        var properties = typeof(TRequest).GetProperties();

        // Loop through each property
        foreach (var property in properties)
        {
            // Check if the property name exists as a query string parameter
            if (!queryStringParams.TryGetValue(property.Name, out var paramValue)) continue;
            
            // Convert the parameter value to the property type
            var convertedValue = paramValue.ToString().ConvertToType(property.PropertyType);

            // Set the property value in the request model
            property.SetValue(requestModel, convertedValue);
        }

        return Task.FromResult(requestModel);
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