using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Optimum.Contracts;

public interface IOptimumBuilder
{
    public IConfiguration Configuration { get; set; }
    public IServiceCollection Services { get; set; }
    public string ServiceId { get;  }
    public string ServiceName { get;  }
    public ILogger Logger { get;  }
    
    bool TryRegister(string name);
    
    void AddBuildAction(Action<IServiceProvider> execute);
    void AddInitializer(IInitializer initializer);
    void AddInitializer<TInitializer>() where TInitializer : IInitializer;
    IOptimumApplicationBuilder Build();
}