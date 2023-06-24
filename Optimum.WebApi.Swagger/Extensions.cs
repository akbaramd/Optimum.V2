using Microsoft.Extensions.DependencyInjection;
using Optimum.Contracts;
using Optimum.Docs.Swagger;

namespace Optimum.WebApi.Swagger;

public static class Extensions
{
   
    
        
    public static IOptimumBuilder AddWebApiSwaggerDocs(this IOptimumBuilder builder, OptimumSwaggerOptions options)
        => builder.AddWebApiSwaggerDocs(b => b.AddSwaggerDocs(options));
        
    private static IOptimumBuilder AddWebApiSwaggerDocs(this IOptimumBuilder builder, Action<IOptimumBuilder> registerSwagger)
    {
        registerSwagger(builder);
        builder.Services.AddSwaggerGen(c => c.DocumentFilter<WebApiDocumentFilter>());
        return builder;
    }
}