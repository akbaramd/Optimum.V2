using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Optimum.Abstractions;
using Optimum.Api.Configurations;
using Optimum.Api.Contracts;
using Optimum.Api.Handlers;
using Optimum.Configures;
using Optimum.CQRS.Contracts;
using Serilog;
using Serilog.Events;

namespace Optimum.Api.CQRS;

public static class Extensions
{
    public static async Task MapPostCommand<TCommand>(this IEndpointConfigure configure, string pattern)
        where TCommand : ICommand
    {
        var dispatcher = configure.WebApplication.Services.GetRequiredService<ICommandDispatcher>();
        configure.WebApplication.MapPost(pattern, async context =>
        {
            var requestBodyString = await new StreamReader(context.Request.Body).ReadToEndAsync();
            var requestBody = JsonConvert.DeserializeObject<TCommand>(requestBodyString) ??
                              throw new InvalidOperationException();
            await dispatcher.SendAsync<TCommand>(requestBody);

            await context.Response.WriteAsJsonAsync(new { Status = true });
        });
    }
}