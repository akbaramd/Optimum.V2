using System.Collections.Concurrent;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Optimum.Contracts;
using Optimum.Initializers;
using Serilog;

namespace Optimum.Builders;

public class OptimumBuilder : IOptimumBuilder
{
    private readonly ConcurrentDictionary<string, bool> _registry = new();
    private readonly List<Action<IServiceProvider>> _buildActions;
    private readonly WebApplicationBuilder _applicationBuilder;
    public OptimumBuilder(string serviceId, string serviceName, WebApplicationBuilder application, ILogger logger)
    {
        _buildActions = new List<Action<IServiceProvider>>();
        _applicationBuilder = application;
        ServiceId = serviceId.ToLower().Replace(" ","-").Replace("_","");
        ServiceName = serviceName;
        Logger = logger;
        Configuration = application.Configuration;
        Services = application.Services;
        
        Services.AddSingleton<IStartupInitializer>(new StartupInitializer());
        
    }

    public IConfiguration Configuration { get; set; }
    public IServiceCollection Services { get; set; }
    public string ServiceId { get;  }
    public string ServiceName { get;  }
    public ILogger Logger { get; }
    public bool TryRegister(string name) => _registry.TryAdd(name, true);

    public void AddBuildAction(Action<IServiceProvider> execute)
        => _buildActions.Add(execute);

    public void AddInitializer(IInitializer initializer)
        => AddBuildAction(sp =>
        {
            var startupInitializer = sp.GetRequiredService<IStartupInitializer>();
            startupInitializer.AddInitializer(initializer);
        });

    public void AddInitializer<TInitializer>() where TInitializer : IInitializer
        => AddBuildAction(sp =>
        {
            var initializer = sp.GetRequiredService<TInitializer>();
            var startupInitializer = sp.GetRequiredService<IStartupInitializer>();
            startupInitializer.AddInitializer(initializer);
        });

    public IOptimumApplicationBuilder Build()
    {
        var serviceProvider = Services.BuildServiceProvider();
        _buildActions.ForEach(a => a(serviceProvider));
        var application =  _applicationBuilder.Build();
        
        using var scope = application.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<IStartupInitializer>();
        Task.Run(() => initializer.InitializeAsync()).GetAwaiter().GetResult();
        
        return new OptimumApplicationBuilder(application);
    }
}