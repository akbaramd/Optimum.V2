using Microsoft.Extensions.DependencyInjection;
using Optimum.Contracts;
using Optimum.CQRS.Contracts;
using Optimum.CQRS.Dispatchers;

namespace Optimum.CQRS;

public static class Extensions
{
    public static void AddCommandHandlers(this IOptimumBuilder builder)
    {
        builder.Services.Scan(scan =>
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            scan.FromAssemblies(assemblies)
                .AddClasses(cls =>
                {
                    cls.AssignableTo(typeof(ICommandHandler<>));
                }).AsImplementedInterfaces()
                .WithTransientLifetime();
        });

    }
    public static void AddQueryHandlers(this IOptimumBuilder builder)
    {
        builder.Services.Scan(scan =>
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            scan.FromAssemblies(assemblies)
                .AddClasses(cls =>
                {
                    cls.AssignableTo(typeof(IQueryHandler<,>));
                }).AsImplementedInterfaces()
                .WithTransientLifetime();
        });

    }
    public static void AddEventHandlers(this IOptimumBuilder builder)
    {
        builder.Services.Scan(scan =>
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            scan.FromAssemblies(assemblies)
                .AddClasses(cls =>
                {
                    cls.AssignableTo(typeof(IEventHandler<>));
                }).AsImplementedInterfaces()
                .WithTransientLifetime();
        });

    }
    public static void AddInMemoryCommandDispatchers(this IOptimumBuilder builder)
    {
        builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
    }
    public static void AddInMemoryQueryDispatchers(this IOptimumBuilder builder)
    {
        builder.Services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
    }
    public static void AddInMemoryEventDispatchers(this IOptimumBuilder builder)
    {
        builder.Services.AddSingleton<IEventDispatcher, EventDispatcher>();
    }

  
}