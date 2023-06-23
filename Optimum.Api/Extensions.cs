using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Optimum.Abstractions;
using Optimum.Api.Configurations;
using Optimum.Api.Contracts;
using Optimum.Api.Handlers;
using Optimum.Configures;
using Serilog;
using Serilog.Events;

namespace Optimum.Api;

public static class Extensions
{
    public static void AddApiEndpoints(this IOptimumService service)
    {
        service.Services.Scan(scan =>
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            scan.FromAssemblies(assemblies)
                .AddClasses(cls =>
                {
                    cls.AssignableTo(typeof(IApiRequestHandler<,>));
                }).AsImplementedInterfaces()
                .WithTransientLifetime();
        });
    }
    
    public static void UseApiEndpoints(this IOptimumApplication application , Action<IEndpointConfigure> configure)
    {
        configure.Invoke(new EndpointApiConfigure(application.Application));
    }
    
    
}