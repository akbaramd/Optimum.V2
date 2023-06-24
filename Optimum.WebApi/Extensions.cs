using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Optimum.Contracts;
using Optimum.WebApi.Configurations;
using Optimum.WebApi.Contracts;
using Optimum.WebApi.Handlers;

namespace Optimum.WebApi;

public static class Extensions
{
    public static IOptimumBuilder AddWebApi(this IOptimumBuilder builder)
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
        
        builder.AddBuildAction(c =>
        {
            Console.WriteLine("sssssssssss");
        });
        
        Console.WriteLine("aaaaaaa");
        return builder;
    }
    
    public static IOptimumApplicationBuilder UseEndpoints(this IOptimumApplicationBuilder builder , Action<IEndpointConfigure> configure , bool authorazation = false)
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
        Console.WriteLine("eeeeeeeeeeeeeeee");
        return builder;
    }
    
    
}