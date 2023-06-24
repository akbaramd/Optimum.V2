using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Optimum.Contracts;
using Optimum.WebApi.Configurations;
using Optimum.WebApi.Contracts;
using Optimum.WebApi.Handlers;
using Serilog;
using Serilog.Events;

namespace Optimum.WebApi;

public static class Extensions
{
    public static void AddWebApi(this IOptimumBuilder builder)
    {
        builder.Services.Scan(scan =>
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            scan.FromAssemblies(assemblies)
                .AddClasses(cls =>
                {
                    cls.AssignableTo(typeof(IRequestHandler<,>));
                }).AsImplementedInterfaces()
                .WithTransientLifetime();
        });

        builder.Services.AddAuthorization();
    }
    
    public static void UseEndpoints(this IOptimumAppBuilder builder , Action<IEndpointConfigure> configure , bool authorazation = false)
    {
        builder.Application.UseRouting();
        builder.Application.UseEndpoints(endpointBuilder =>
        {
            configure.Invoke(new EndpointApiConfigure(endpointBuilder, endpointBuilder.ServiceProvider));
        });

        if (authorazation)
        {
            builder.Application.UseAuthorization();
        }
    }
    
    
}