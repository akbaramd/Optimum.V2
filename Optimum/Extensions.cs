using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Optimum.Abstractions;
using Optimum.Configures;
using Serilog;
using Serilog.Events;

namespace Optimum;

public static class Extensions
{
    public static void AddOptimum(this IServiceCollection services,string serviceId , string serviceName,Action<IOptimumService> configure)
    {

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .Filter.ByExcluding(x=>x.Level == LogEventLevel.Information)
            .CreateLogger();
        
        Log.Debug("[Optimum] Initialize Service {ServiceName} ({ServiceId})", serviceName, serviceId);
        
        var configuration = new OptimumService(serviceId, serviceName, services,Log.Logger);
        
        configure.Invoke(configuration);
    }
    
    
    public static void UseOptimum(this WebApplication app,Action<IOptimumApplication> configure)
    {
        var configuration = new OptimumApplication(app);
        configure.Invoke(configuration);
    }
    
    
    public static object ConvertToType(this string value, Type targetType)
    {
        if (targetType == typeof(string))
        {
            return value;
        }
        else if (targetType == typeof(int))
        {
            if (int.TryParse(value, out int intValue))
            {
                return intValue;
            }
        }
        else if (targetType == typeof(bool))
        {
            if (bool.TryParse(value, out bool boolValue))
            {
                return boolValue;
            }
        }
        else if (targetType == typeof(DateTime))
        {
            if (DateTime.TryParse(value, out DateTime dateTimeValue))
            {
                return dateTimeValue;
            }
        }

        // Add more custom type conversions as needed

        return null; // or throw an exception if conversion fails
    }
    
    public static bool IsJson(this string input)
    {
        try
        {
            JsonConvert.DeserializeObject(input);
            return true;
        }
        catch (JsonReaderException)
        {
            return false;
        }
    }
}