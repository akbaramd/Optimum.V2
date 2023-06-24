using Microsoft.Extensions.DependencyInjection;
using Optimum.Contracts;
using Optimum.CQRS.Contracts;
using Optimum.CQRS.Dispatchers;

namespace Optimum.CQRS;

public static class Extensions
{
    public static IOptimumBuilder AddCommandHandlers(this IOptimumBuilder builder)
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
        return builder;

    }
    public static IOptimumBuilder AddQueryHandlers(this IOptimumBuilder builder)
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
        return builder;
    }
    public static IOptimumBuilder AddEventHandlers(this IOptimumBuilder builder)
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
        return builder;
    }
    public static IOptimumBuilder AddInMemoryCommandDispatchers(this IOptimumBuilder builder)
    {
        builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        return builder;
    }
    public static IOptimumBuilder AddInMemoryQueryDispatchers(this IOptimumBuilder builder)
    {
        builder.Services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        return builder;
    }
    public static IOptimumBuilder AddInMemoryEventDispatchers(this IOptimumBuilder builder)
    {
        builder.Services.AddSingleton<IEventDispatcher, EventDispatcher>();
        return builder;
    }

  
}