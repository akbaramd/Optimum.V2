using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Optimum.Contracts;

namespace Optimum;

public static class Extension
{
    // public static void AddOptimum(this IServiceCollection services,string serviceId , string serviceName,Action<IOptimumBuilder> configure)
    // {
    //
    //     Log.Logger = new LoggerConfiguration()
    //         .MinimumLevel.Debug()
    //         .WriteTo.Console()
    //         .Filter.ByExcluding(x=>x.Level == LogEventLevel.Information)
    //         .CreateLogger();
    //     
    //     Log.Debug("[Optimum] Initialize Service {ServiceName} ({ServiceId})", serviceName, serviceId);
    //     
    //     var configuration = new OptimumBuilder(serviceId, serviceName, services,Log.Logger);
    //     
    //     configure.Invoke(configuration);
    // }
    //
    //
    // public static void UseOptimum(this WebApplication app,Action<IOptimumApplication> configure)
    // {
    //     var configuration = new Implemantions.OptimumApplication(app);
    //     configure.Invoke(configuration);
    // }
    
    public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName)
        where TModel : new()
    {
        var model = new TModel();
        configuration.GetSection(sectionName).Bind(model);
        return model;
    }

    public static TModel GetOptions<TModel>(this IOptimumBuilder builder, string settingsSectionName)
        where TModel : new()
    {
        if (builder.Configuration is not null)
        {
            return builder.Configuration.GetOptions<TModel>(settingsSectionName);
        }

        using var serviceProvider = builder.Services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<TModel>(settingsSectionName);
    }
}