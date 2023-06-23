using Microsoft.Extensions.DependencyInjection;

using Optimum.Abstractions;
using Serilog;

namespace Optimum.Configures;

public class OptimumService : IOptimumService
{
    public OptimumService(string serviceId, string serviceName, IServiceCollection services, ILogger logger)
    {
        ServiceId = serviceId.ToLower().Replace(" ","-").Replace("_","");
        ServiceName = serviceName;
        Services = services;
        Logger = logger;
    }

    public string ServiceId { get;  }
    public string ServiceName { get;  }
    public IServiceCollection Services { get; }
    public ILogger Logger { get; }
}