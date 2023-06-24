using Microsoft.Extensions.DependencyInjection;
using Optimum.Contracts;
using Serilog;

namespace Optimum.Implemantions;

public class OptimumBuilder : IOptimumBuilder
{
    public OptimumBuilder(string serviceId, string serviceName, IServiceCollection services, ILogger logger)
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