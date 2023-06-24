using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Optimum.Contracts;
using Optimum.Docs.Swagger;
using Swashbuckle.AspNetCore.Swagger;

namespace Optimum.WebApi.Swagger;

public static class Extensions
{
    private const string SectionName = "swagger";
    private const string RegistryName = "docs.swagger";

    public static IOptimumBuilder AddSwaggerDocs(this IOptimumBuilder builder, OptimumSwaggerOptions options)
    {
        if (!options.Enabled && !builder.TryRegister(RegistryName))
        {
            return builder;
        }

        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static IOptimumApplicationBuilder UseSwaggerDocs(this IOptimumApplicationBuilder builder)
    {
        var options = builder.Application.Services.GetRequiredService<OptimumSwaggerOptions>();
        if (!options.Enabled)
        {
            return builder;
        }

        builder.Application.UseSwagger();
        builder.Application.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
        return builder;
    }

    /// <summary>
    /// Replaces leading double forward slash caused by an empty route prefix
    /// </summary>
    /// <param name="route"></param>
    /// <returns></returns>
    private static string FormatEmptyRoutePrefix(this string route)
    {
        return route.Replace("//","/");
    }
}