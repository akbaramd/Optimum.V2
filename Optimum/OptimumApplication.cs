using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Optimum.Builders;
using Optimum.Contracts;
using Serilog;
using Serilog.Events;

namespace Optimum;

public abstract class OptimumApplication
{
    public static IOptimumBuilder CreateServiceBuilder(string serviceId, string serviceName, string[]? args = null)
    {
        var builder = WebApplication.CreateBuilder(args?? new []{""});
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .Filter.ByExcluding(x => x.Level == LogEventLevel.Information)
            .CreateLogger();
        builder.Logging.ClearProviders().AddSerilog();
        return new OptimumBuilder(serviceId, serviceName, builder, Log.Logger);
    }
}