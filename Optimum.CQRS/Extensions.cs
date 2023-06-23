using Microsoft.Extensions.DependencyInjection;
using Optimum.Abstractions;
using Optimum.CQRS.Contracts;
using Optimum.CQRS.Dispatchers;

namespace Optimum.CQRS;

public static class Extensions
{
    public static void AddCQRS(this IOptimumService service)
    {
        service.Services.Scan(scan =>
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            scan.FromAssemblies(assemblies)
                .AddClasses(cls =>
                {
                    cls.AssignableTo(typeof(ICommandHandler<>));
                }).AsImplementedInterfaces()
                .WithTransientLifetime();
        });

        service.Services.AddTransient<ICommandDispatcher, CommandDispatcher>();
    }
    
  
}