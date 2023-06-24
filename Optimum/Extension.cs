using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Optimum.Contracts;
using Optimum.Implemantions;
using Serilog;
using Serilog.Events;

namespace Optimum;

public static class Extension
{
    public static void AddOptimum(this IServiceCollection services,string serviceId , string serviceName,Action<IOptimumBuilder> configure)
    {

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .Filter.ByExcluding(x=>x.Level == LogEventLevel.Information)
            .CreateLogger();
        
        Log.Debug("[Optimum] Initialize Service {ServiceName} ({ServiceId})", serviceName, serviceId);
        
        var configuration = new OptimumBuilder(serviceId, serviceName, services,Log.Logger);
        
        configure.Invoke(configuration);
    }
    
    
    public static void UseOptimum(this WebApplication app,Action<IOptimumAppBuilder> configure)
    {
        var configuration = new OptimumAppBuilder(app);
        configure.Invoke(configuration);
    }
}